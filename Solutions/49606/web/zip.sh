#!/bin/bash

#functions:
exit_if_operation_failed() {
  if [ "$1" != 0 ]; then
    echo "Error! Operation exit with code $1: $2"
    exit "$1"
  fi
}
#===========================================================

# Get Inputs
target="$1"
if [ -z "$target" ]; then
  printf "Target file or directory: "
  read -r target
fi

if [ ! -f "$target" ] && [ ! -d "$target" ]; then
  echo "File or directory path is not valid."
  exit 1
fi

output="$2"
if [ -z "$output" ]; then
  output="output.zip"
fi
#===========================================================

zip -r "$output" "$target"
exit_if_operation_failed "$?" "Can not create zip file from $target to $output"
wait

echo "The operation was successful."
echo "Output file: $output"
