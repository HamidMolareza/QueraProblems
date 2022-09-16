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
# Get Inputs
queraId="$1"
if [ -z "$queraId" ]; then
    printf "Quera Id: "
    read -r queraId
fi

templateDir="$2"
if [ -z "$templateDir" ]; then
    if [ ! -d "$templateDir" ]; then
        printf "Template directory: "
        read -r templateDir
    fi
fi
if [ ! -d "$templateDir" ]; then
    echo "Error! Can not find template dir in $templateDir"
    exit 1
fi

solutionsDir="$3"
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
#===========================================================

git checkout -b "$queraId"
exit_if_operation_failed "$?" "Can not checkout to $queraId"

create_dir_if_is_not_exist "$solutionsDir/$queraId"

resultDir="$solutionsDir/$queraId"
cp -r "$templateDir" "$resultDir"
exit_if_operation_failed "$?" "Can not copy template from $templateDir to $resultDir"
wait

echo "Directory is ready: $resultDir"
