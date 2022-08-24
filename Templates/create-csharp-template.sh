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

csharpTemplateDir="$3"
if [ -z "$csharpTemplateDir" ]; then
    csharpTemplateDir="csharp"
    if [ ! -d "$csharpTemplateDir" ]; then
        printf "csharp template directory: "
        read -r csharpTemplateDir
    fi
fi
if [ ! -d "$csharpTemplateDir" ]; then
    echo "Error! Can not find csharp template dir in $csharpTemplateDir"
    exit 1
fi
#===========================================================

git checkout -b "$queraId"
exit_if_operation_failed "$?" "Can not checkout to $queraId"

create_dir_if_is_not_exist "$solutionsDir/$queraId"

resultDir="$solutionsDir/$queraId"
cp -r "$csharpTemplateDir" "$resultDir"
exit_if_operation_failed "$?" "Can not copy template from $csharpTemplateDir to $resultDir"
wait

echo "Directory is ready: $resultDir"
