#!/bin/bash

#functions:
exit_if_operation_failed() {
  if [ "$1" != 0 ]; then
    echo "Operation exit with code $1: $2"
    exit "$1"
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

checkout "master"
exit_if_operation_failed "$?" "Checkout to master branch failed."
wait

git merge "$queraId"
exit_if_operation_failed "$?" "Merge $queraId to master branch failed."
wait

echo "The operation was completed successfully."
printf "Do you want delete branch $queraId (y/N): "
read -r deleteBranch
if [ "$deleteBranch" != 'y' ] && [ "$deleteBranch" != 'Y' ]; then
  exit 0
fi

git branch --delete "$queraId"
if [ "$?" != 0]; then
  echo "Warning! Operation failed."
fi
