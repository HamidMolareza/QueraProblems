reserved_usernames = ["quera", "codecup"]


def check_registration_rules(**kwargs):
    valid_usernames = []
    for username, password in kwargs.items():
        if username in reserved_usernames:
            continue
        if len(username) < 4 or len(password) < 6:
            continue
        if password.isdigit():  # Check if the password contains only numbers
            continue
        valid_usernames.append(username)
    return valid_usernames
