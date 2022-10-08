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

validate_quera_id() {
  quera_id="$1"

  printf "Validating Quera Id... "
  status_code=$(curl -s -o /dev/null -w "%{http_code}" --max-time 10 https://quera.org/problemset/"$quera_id"/)
  if [ "$status_code" != "200" ]; then
    echo "Error! It seems that the ID is not valid. (Id: $quera_id, status code: $status_code)"
    return 1
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

question_ignore_error() {
  if [ "$?" != 0 ]; then
    printf "Do you want to ignore this error?(Y/n) "
    read -r ignore_error
    if [ "$ignore_error" = 'n' ] || [ "$ignore_error" = 'N' ]; then
      exit 0
    fi
  fi
}
#===========================================================
# Get Inputs
quera_id="$1"
if [ -z "$quera_id" ]; then
  printf "Quera Id: "
  read -r quera_id
fi
validate_quera_id "$quera_id"
question_ignore_error

template_dir="$2"
if [ -z "$template_dir" ]; then
  if [ ! -d "$template_dir" ]; then
    printf "Template directory: "
    read -r template_dir
  fi
fi
if [ ! -d "$template_dir" ]; then
  echo "Error! Can not find template dir in $template_dir"
  exit 1
fi

ide="$3"
if [ -z "$ide" ]; then
  printf "Ide (like code, rider, etc): "
  read -r ide
fi
ensure_ide_is_valid "$ide"

solutions_dir="$4"
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
cp -r "$template_dir" "$result_dir"
exit_if_operation_failed "$?" "Can not copy template from $template_dir to $result_dir"
wait

echo "Directory is ready: $result_dir"
$ide "$result_dir" >/dev/null
warning_if_operation_failed "$?" "Can not open your ide for $result_dir"

echo ""
printf "Do you want merge this branch to master branch?(y/N) "
read -r merge_confirm
if [ "$merge_confirm" = 'y' ] || [ "$merge_confirm" = 'Y' ]; then
  ./merge-into-master-branch.sh "$quera_id" "y"
fi

printf "Do you want push master branch?(y/N) "
read -r push_confirm
if [ "$push_confirm" = 'y' ] || [ "$push_confirm" = 'Y' ]; then
  git pull --rebase
  git push
fi
