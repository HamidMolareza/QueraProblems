#!/bin/bash

warning_if_operation_failed() {
  if [ "$1" != 0 ]; then
    echo "Operation failed with code $1: $2"
  fi
}

# Get inputs and Validations
branchesFile="$1"
if [ -z "$branchesFile" ]; then
  printf "Branches file: "
  read -r branchesFile
fi
if [ ! -f "$branchesFile" ]; then
  echo "Can not find branches file in $branchesFile."
  exit 1
fi

gitDir="$2"
if [ -z "$gitDir" ]; then
  printf "Git directory: "
  read -r gitDir
fi
if [ ! -d "$gitDir" ]; then
  echo "Git directory not found in $gitDir."
  exit 1
fi

confirm="$3"
if [ -z "$confirm" ]; then
  printf "Are you sure you want to delete all branches of $branchesFile both locally and remotely? (y/N): "
  read -r confirm
fi
if [ "$confirm" != 'y' ] && [ "$confirm" != 'Y' ]; then
  exit 0
fi
#===========================================================

while IFS= read -r branchName; do

  #  delete branch locally
  git --git-dir "$gitDir" branch -D "$branchName" --quiet
  warning_if_operation_failed "$?" "delete branch locally - branch name: $branchName"
  wait

  #   delete branch remotely
  #  git --git-dir "$gitDir" push origin --delete "$branchName"
  #  warning_if_operation_failed "$?" "delete branch remotely - branch name: $branchName"
  #  wait

done <"$branchesFile"
