import os.path
import sys
from os.path import basename
from typing import Optional
from urllib import request, parse


def url2name(url: str) -> str:
    return basename(parse.urlsplit(url)[2])


def get_file_name(url: str, request_url_open, local_file_name: Optional[str] = None):
    if local_file_name is not None:
        return local_file_name
    name = url2name(url)
    if name:
        return name
    if request_url_open.url != url:
        # if we were redirected, the real file name we take from the final URL
        return url2name(request_url_open.url)
    if 'Content-Disposition' in request_url_open.info():
        # If the response has Content-Disposition, we take file name from it
        name = request_url_open.info(
        )['Content-Disposition'].split('filename=')[1].split(";")[0]
        if name[0] == '"' or name[0] == "'":
            name = name[1:-1]
        if name[0] == 'b':
            name = name[2:-1]
        return name
    raise Exception("Can not detect file name.")


def download(url: str, directory: str, local_file_name: Optional[str] = None) -> str:
    req = request.Request(url)
    request_url_open = request.urlopen(req)

    file_name = get_file_name(url, request_url_open, local_file_name)

    if not os.path.exists(directory):
        os.makedirs(directory)
    output = os.path.join(directory, file_name)

    f = open(output, 'wb')
    f.write(request_url_open.read())
    f.close()
    return output


def get_argument(index: int, message: str) -> str:
    if len(sys.argv) > index:
        return sys.argv[index]
    else:
        return input(f"{message}")


def main():
    url = get_argument(1, "Url: ")
    directory = get_argument(2, "Directory: ")

    output = download(url, directory)
    print(output)


if __name__ == "__main__":
    main()
