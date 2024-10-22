.PHONY: help run

run:  ## Run the program.js with inputs
	docker run --rm -it -v ${PWD}:/src/ hamidmolareza/d8 run /src/program.js -d /src/inputs

help: ## Display this help message
	@echo "Usage: make <target>"
	@echo ""
	@echo "Targets:"
	@awk -F ':|##' '/^[^\t].+?:.*?##/ { printf "  %-20s %s\n", $$1, $$NF }' $(MAKEFILE_LIST) | sort

.DEFAULT_GOAL := help
