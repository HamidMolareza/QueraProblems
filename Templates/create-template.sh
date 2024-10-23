#!/bin/bash

show_help() {
  cat <<EOF
Usage: $(basename "$0") [options]

Options:
  -q, --quera-id        Quera ID (required)
  -t, --template  Template directory (optional)
  -i, --ide  IDE (e.g. code, rider) (optional)
  -o, --output    Solutions directory (optional, default: Solutions or ../Solutions)
  -d, --download  Download link for base project (optional)
  -h, --help      Display this help message

Example:
  $(basename "$0") -i 12345 -t /path/to/template -o /path/to/solutions -d http://example.com/project.zip
EOF
  exit 0
}

# Functions:
exit_if_failed() {
  if [ "$1" -ne 0 ]; then
    echo "Error! Operation failed with code $1: $2"
    exit "$1"
  fi
}

warn_if_failed() {
  if [ "$1" -ne 0 ]; then
    echo "Warning! Operation failed with code $1: $2"
  fi
}

create_dir_if_missing() {
  [ ! -d "$1" ] && mkdir -p "$1"
}

validate_quera_id() {
  local quera_id="$1"
  printf "Validating Quera ID... "
  status_code=$(curl -s -o /dev/null -w "%{http_code}" --max-time 10 "https://quera.org/problemset/$quera_id")

  if [ "$status_code" -ne 200 ]; then
    echo "Error! Invalid ID. (ID: $quera_id, Status: $status_code)"
    return 1
  fi
  echo "done."
}

ensure_command_exists() {
  command -v "$1" &>/dev/null || {
    echo "Command not found: $1"
    exit 1
  }
}

ask_ignore_error() {
  if [ "$?" -ne 0 ]; then
    read -rp "Do you want to ignore this error? (Y/n) " ignore_error
    [[ "$ignore_error" =~ ^[nN]$ ]] && exit 1
  fi
}

get_default_solution_dir() {
  for dir in "Solutions" "../Solutions"; do
    [ -d "$dir" ] && echo "$dir" && return
  done
  echo ""  # Not Found
}

decompress_if_need(){
  local file="$1"
  local output="$2"


# Detect if the file is compressed (zip, rar)
file_type=$(file --mime-type -b "$file")

  case $file_type in
      application/zip|application/x-rar)
          read -rp "The file is compressed ($file_type). Do you want to extract it? (y/n): " choice
          if [[ "$choice" == "y" || "$choice" == "Y" ]]; then
              case $file_type in
                  application/zip)
                      unzip "$file" -d "$output"
                      ;;
                  application/x-rar)
                      if ! command -v unrar &> /dev/null; then
                          echo "unrar is not installed. Please install it with: sudo apt install unrar"
                          return 1
                      fi
                      unrar x "$file" "$output/"
                      ;;
              esac
              echo "Extraction completed."
          else
              echo "Skipping extraction."
          fi
          ;;
      *)
          echo "The file is not compressed or has an unsupported format."
          ;;
  esac
}

#===========================================================
# Parse Command-Line Options
while [[ $# -gt 0 ]]; do
  case "$1" in
    -q|--quera-id)
      quera_id="$2"
      shift 2
      ;;
    -t|--template)
      template_dir="$2"
      shift 2
      ;;
    -o|--output)
      solutions_dir="$2"
      shift 2
      ;;
    -d|--download)
      download_link="$2"
      shift 2
      ;;
    -i|--ide)
      ide="$2"
      shift 2
      ;;
    -h|--help)
      show_help
      ;;
    *)
      echo "Unknown option: $1"
      show_help
      ;;
  esac
done

#===========================================================
# Validate Inputs
[ -z "$quera_id" ] && { echo "Error: Quera ID is required."; show_help; }
validate_quera_id "$quera_id"
ask_ignore_error

if [ -n "$template_dir" ] && [ ! -d "$template_dir" ]; then
  echo "Invalid template path: $template_dir"
  exit 1;
fi

solutions_dir="${solutions_dir:-$(get_default_solution_dir)}"
if [ -z "$solutions_dir" ] || [ ! -d "$solutions_dir" ]; then
  echo "Solutions directory not found: $solutions_dir"
  exit 1
fi

if [ -n "$ide" ]; then
  ensure_command_exists "$ide"
fi

#===========================================================

# Checkout a new branch
git checkout -b "$quera_id"
exit_if_failed "$?" "Unable to checkout to branch $quera_id"

# Create solution directory
target_solution_dir="$solutions_dir/$quera_id"
create_dir_if_missing "$target_solution_dir"

# Copy template to solution directory
if [ -n "$template_dir" ]; then
  cp -r "$template_dir" "$target_solution_dir"
  exit_if_failed "$?" "Failed to copy template to $target_solution_dir"
fi

# Download base project (optional)
if [ -n "$download_link" ]; then
  echo "Downloading..."
  output_file=$(curl -L -o "$download_link" "$target_solution_dir" 2>&1)
  warn_if_failed "$?" "Failed to download project from $download_link"
fi

# Unzip base project if it's a ZIP file
decompress_if_need "$output_file" "$target_solution_dir"

echo "Directory is ready: $target_solution_dir"

if [ -n "$ide" ]; then
  "$ide" "$target_solution_dir" &
  warn_if_failed "$?" "Failed to open IDE for $target_solution_dir"
fi

# Push to master branch (optional)
read -rp "Do you want to push to the master branch? (y/N) " push_confirm
if [[ "$push_confirm" =~ ^[yY]$ ]]; then
  git pull --rebase && git push
fi
