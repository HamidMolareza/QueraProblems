#!/bin/bash

#functions:
exit_if_operation_failed() {
  if [ "$1" != 0 ]; then
    echo "Operation exit with code $1: $2"
    exit "$1"
  fi
}

create_dir_if_is_not_exist() {
  if [ ! -d "$1" ]; then
    mkdir "$1"
  fi
}
#===========================================================

#Get inputs
if [ "$#" != 3 ]; then
  printf "Quera Directory: "
  read -r queraDir

  printf "Output Directory: "
  read -r outputDir

  printf "Branches File: "
  read -r branchesFile

else
  queraDir="$1"
  outputDir="$2"
  branchesFile="$3"
fi
gitDir="$queraDir/.git"

#Validations:
if [ ! -d "$gitDir" ]; then
  echo "Git directory not found in $gitDir."
  exit 1
fi

if [ ! -f "$branchesFile" ]; then
  echo "Can not find branches file in $branchesFile."
  exit 1
fi
#===========================================================

create_dir_if_is_not_exist "$outputDir"
exit_if_operation_failed $? "Create directory in $outputDir failed."

while IFS= read -r branchName; do
  #checkout
  git --git-dir "$gitDir" checkout "$branchName" --quiet
  exit_if_operation_failed $? "Checkout Error - branch name: $branchName"
  wait

  #Create dir
  create_dir_if_is_not_exist "$outputDir/$branchName"
  wait

  #Copy files
  solutionDir="$outputDir/$branchName"
  cp -rf "$queraDir/Quera" "$solutionDir/" && cp -f "$queraDir/README.md" "$solutionDir/Quera"
  exit_if_operation_failed $? "Copy Error - branch name: $branchName"
  wait

  #Change Quera name
  mv "$solutionDir/Quera" "$solutionDir/csharp"
  exit_if_operation_failed $? "Change directory name Error - branch name: $branchName"
  wait

done <"$branchesFile"
