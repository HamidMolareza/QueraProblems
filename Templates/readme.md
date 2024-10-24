# create-template script

## Overview

This script automates setting up a solution directory for a specified Quera ID. It can validate the Quera problem ID, create a new Git branch, copy a template directory, download a base project, decompress the downloaded file if necessary, and open the solution in a specified IDE.

## Prerequisites

- **Git** installed and configured.
- **curl** for validating Quera IDs and downloading files.
- **unzip** (for handling zip files) or **unrar** (for handling rar files) if dealing with compressed base projects.
- The specified IDE (if provided) must be installed and in the system’s PATH.

## Usage

```bash
chmod +x create-template.sh
./create-template.sh --help
```

### Options

- `-q, --quera-id` **[required]**  
  The ID of the problem on Quera.

- `-t, --template` **[optional]**  
  The path to the template directory to copy into the solution folder.

- `-i, --ide` **[optional]**  
  The command to open an IDE (e.g., `code`, `rider`). The IDE must be available in the system's PATH.

- `-o, --output` **[optional]**  
  The directory to store solutions. Defaults to the "Solutions" directory or "../Solutions" if not specified.

- `-d, --download` **[optional]**  
  The download link for a base project archive (zip or rar).

- `-h, --help`  
  Displays a help message with usage information.

### Example

```bash
./script.sh -q 12345 -t /path/to/template -o /path/to/solutions -d http://example.com/project.zip -i code
```

## How It Works

1. **Parse Command-Line Arguments**  
   Parses the provided options using standard bash syntax.

2. **Validate Quera ID**  
   Verifies the Quera ID by making an HTTP request to `https://quera.org/problemset/<quera_id>`. It checks for a status code of 200.

3. **Create or Validate Directory**  
   Creates the output directory if it does not already exist.

4. **Checkout New Git Branch**  
   Creates a new Git branch named after the provided Quera ID.

5. **Copy Template (if provided)**  
   Copies the specified template directory to the solution folder.

6. **Download Project (if URL provided)**  
   Downloads a file from the provided URL and saves it in the solution folder.

7. **Decompress File (if necessary)**  
   If the downloaded file is a compressed archive (zip or rar), the script prompts to extract it.

8. **Open in IDE (if specified)**  
   Opens the solution directory in the specified IDE.

9. **Push Changes (Optional)**  
   Prompts the user to confirm whether to push the changes to the master branch with a pull/merge.

## Notes

- The script relies on some common Linux commands (`curl`, `git`, `mkdir`, etc.). Make sure these are available.
- For handling rar files, the **unrar** utility needs to be installed separately (`sudo apt install unrar`).
- The download step requires a direct download link to a zip or rar file.
