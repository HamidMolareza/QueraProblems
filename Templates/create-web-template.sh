#!/bin/bash

#functions:
exit_if_operation_failed() {
  if [ "$1" != 0 ]; then
    echo "Error! Operation exit with code $1: $2"
    exit "$1"
  fi
}

warning_if_operation_failed() {
  if [ "$1" != 0 ]; then
    echo "Warning! Operation exit with code $1: $2"
  fi
}

create_dir_if_is_not_exist() {
  if [ ! -d "$1" ]; then
    mkdir "$1"
  fi
}

ensure_quera_id_is_valid() {
  quera_id="$1"
  
  printf "Validating Quera Id... "
  status_code=$(curl -s -o /dev/null -w "%{http_code}" https://quera.org/problemset/"$quera_id"/)
  if [ "$status_code" != "200" ]; then
    echo "Error! It seems that the ID is not valid. (Id: $quera_id, status code: $status_code)"
    exit 1
  fi
  echo "done."
}

ensure_ide_is_valid() {
  ide="$1"
  if ! command -v "$ide" &>/dev/null; then
    echo "the ide command could not be found. (Ide: $ide)"
    exit 1
  fi
}
#===========================================================
# Get Inputs
quera_id="$1"
if [ -z "$quera_id" ]; then
  printf "Quera Id: "
  read -r quera_id
fi

web_template_dir="$2"
if [ -z "$web_template_dir" ]; then
  web_template_dir="web"
  if [ ! -d "$web_template_dir" ]; then
    printf "web template directory: "
    read -r web_template_dir
  fi
fi
if [ ! -d "$web_template_dir" ]; then
  echo "Error! Can not find web template dir in $web_template_dir"
  exit 1
fi

download_link="$3"
if [ -z "$download_link" ]; then
  printf "Download link for base project (Optional): "
  read -r download_link
fi

ide="$4"
if [ -z "$ide" ]; then
  printf "Ide (like code, rider, etc): "
  read -r ide
fi
ensure_ide_is_valid "$ide"

solutions_dir="$5"
if [ -z "$solutions_dir" ]; then
  solutions_dir="../Solutions"
  if [ ! -d "$solutions_dir" ]; then
    printf "Solutions directory: "
    read -r solutions_dir
  fi
fi
if [ ! -d "$solutions_dir" ]; then
  echo "Error! Can not find solutions dir in $solutions_dir"
  exit 1
fi
#===========================================================

#checkout
git checkout -b "$quera_id"
exit_if_operation_failed "$?" "Can not checkout to $quera_id"

#create solution dir
create_dir_if_is_not_exist "$solutions_dir/$quera_id"

#copy template to solution dir
result_dir="$solutions_dir/$quera_id"
cp -r "$web_template_dir" "$result_dir"
exit_if_operation_failed "$?" "Can not copy template from $web_template_dir to $result_dir"
wait

#download template from link
if [ -z "$download_link" ]; then
  echo "The download link is not given, so skip download step."
else
  projectFile="$result_dir/project.zip"
  if [ -f "$projectFile" ]; then
    echo "The project file already exists."
  else
    curl --silent --output "$projectFile" "$download_link"
    warning_if_operation_failed "$?" "Can not download project from '$download_link' to '$projectFile'"
    wait
  fi
fi

echo "Directory is ready: $result_dir"
$ide "$result_dir" >/dev/null
warning_if_operation_failed "$?" "Can not open your ide for $result_dir"

echo ""
printf "Do you want merge this branch to master branch?(y/N) "
read -r merge_confirm
if [ "$merge_confirm" = 'y' ] || [ "$merge_confirm" = 'Y' ]; then
  ./merge-into-master-branch.sh "$quera_id" "y"
fi
