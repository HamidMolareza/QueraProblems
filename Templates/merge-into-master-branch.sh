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
#===========================================================

#checkout to master
git checkout "master" --quiet
exit_if_operation_failed "$?" "Checkout to master branch failed."
wait

git merge "$queraId" --quiet
exit_if_operation_failed "$?" "Merge $queraId to master branch failed."
wait

printf "Do you want delete branch $queraId (y/N): "
read -r deleteBranch
if [ "$deleteBranch" -eq 'y' ] && [ "$deleteBranch" -eq 'Y' ]; then
  git branch --delete "$queraId" --quiet
  warning_if_operation_failed "$?" "delete branch $queraId failed."
fi

echo "The operation was completed successfully."
echo "Please don't forget to push commits."