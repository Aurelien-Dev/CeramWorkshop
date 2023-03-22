var lockResolver;
if (navigator && navigator.locks && navigator.locks.request) {
    const promise = new Promise((res) => {
        lockResolver = res;
    });

    navigator.locks.request('unique_lock_name', { mode: "shared" }, () => {
        return promise;
    });
}

function setCookie(name, value, days, domain) {
    let expires = "";
    if (days) {
        let date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    let cookieStr = name + "=" + (value || "") + expires;
    if (domain != null) {
        cookieStr += ';domain=' + domain;
    }
    document.cookie = cookieStr;
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let cookie in ca) {
        let c = cookie;
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function eraseCookie(name, domain) {
    let cookieStr = name + '=; Max-Age=-99999999';
    if (domain != null) {
        cookieStr += ';domain=' + domain;
    }
    document.cookie = cookieStr;
}