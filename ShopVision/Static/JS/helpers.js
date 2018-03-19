function LimitCharacters(input,maxLength) {
    if (maxLength < 1) {
        maxLength = 1;
    }

    if (input.length < maxLength) {
        return input;
    }

    return input.substring(0,maxLength) + "[..]";
}

// Programmatically loads a CSS file
function LoadCSSFile(filename) {
    var fileref=document.createElement("link")
    fileref.setAttribute("rel", "stylesheet")
    fileref.setAttribute("type", "text/css")
    fileref.setAttribute("href", filename)

    if (typeof fileref!="undefined") {
        document.getElementsByTagName("head")[0].appendChild(fileref)
    }
}

// Programmatically loads a JS file
function LoadJSFile(filename) {
    var fileref=document.createElement('script')
    fileref.setAttribute("type","text/javascript")
    fileref.setAttribute("src", filename)

    if (typeof fileref!="undefined") {
        document.getElementsByTagName("head")[0].appendChild(fileref)
    }
}