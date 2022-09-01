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
#===========================================================
# Get Inputs
queraId="$1"
if [ -z "$queraId" ]; then
  printf "Quera Id: "
  read -r queraId
fi

solutionsDir="$2"
if [ -z "$solutionsDir" ]; then
  solutionsDir="../Solutions"
  if [ ! -d "$solutionsDir" ]; then
    printf "Solutions directory: "
    read -r solutionsDir
  fi
fi
if [ ! -d "$solutionsDir" ]; then
  echo "Error! Can not find solutions dir in $solutionsDir"
  exit 1
fi

webTemplateDir="$3"
if [ -z "$webTemplateDir" ]; then
  webTemplateDir="web"
  if [ ! -d "$webTemplateDir" ]; then
    printf "web template directory: "
    read -r webTemplateDir
  fi
fi
if [ ! -d "$webTemplateDir" ]; then
  echo "Error! Can not find web template dir in $webTemplateDir"
  exit 1
fi

downloadLink="$4"
if [ -z "$downloadLink" ]; then
  printf "Download link for base project (Optional): "
  read -r downloadLink
fi
#===========================================================

#checkout
git checkout -b "$queraId"
exit_if_operation_failed "$?" "Can not checkout to $queraId"

#create solution dir
create_dir_if_is_not_exist "$solutionsDir/$queraId"

#copy template to solution dir
resultDir="$solutionsDir/$queraId"
cp -r "$webTemplateDir" "$resultDir"
exit_if_operation_failed "$?" "Can not copy template from $webTemplateDir to $resultDir"
wait

#download template from link
if [ -z "$downloadLink" ]; then
  echo "The download link is not given, so skip download step."
else
  projectFile="$resultDir/project.zip"
  if [ -f "$projectFile" ]; then
    echo "The project file already exists."
  else
    curl --silent --output "$projectFile" "$downloadLink"
    warning_if_operation_failed "$?" "Can not download project from '$downloadLink' to '$projectFile'"
    wait
  fi
fi

echo "Directory is ready: $resultDir"
