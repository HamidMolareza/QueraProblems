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
#===========================================================

# Get Inputs
queraId="$1"
if [ -z "$queraId" ]; then
  printf "Quera Id: "
  read -r queraId
fi

deleteBranch="$2"
#===========================================================

#checkout to master branch
git checkout "master" --quiet
exit_if_operation_failed "$?" "Checkout to master branch failed."
wait

#merge into master branch
git merge "$queraId" --quiet
exit_if_operation_failed "$?" "Merge $queraId to master branch failed."
wait

echo "The operation was completed successfully."

#delete branch
if [ -z "$deleteBranch" ]; then
  printf "Do you want delete branch $queraId (y/N): "
  read -r deleteBranch
fi
if [ "$deleteBranch" == 'y' ] || [ "$deleteBranch" == 'Y' ]; then
  git branch -D "$queraId" --quiet
  warning_if_operation_failed "$?" "delete branch $queraId failed."
fi

echo "Please don't forget to push commits."
