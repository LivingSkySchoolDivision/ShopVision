function replaceEmotes(input) {
    var str = input;
    //str = str.replace("bacon","<img src=\"/dashboard_common/emoticons/bacon.gif\">");
    str = str.replace(/bacon/gi, function myReplaceFunction(x) { return "<img src=\"/dashboard_common/emoticons/bacon.gif\">"});
    str = str.replace(/pizza/gi, function myReplaceFunction(x) { return "<img src=\"/dashboard_common/emoticons/pizza.png\">"});
    str = str.replace(/nyan/gi, function myReplaceFunction(x) { return "<img src=\"/dashboard_common/emoticons/nyan_cat.gif\">"});
    str = str.replace(/slurpee/gi, function myReplaceFunction(x) { return "<img src=\"/dashboard_common/emoticons/slurpee.png\">"});
    str = str.replace(/tacos/gi, function myReplaceFunction(x) { return "<img src=\"/dashboard_common/emoticons/taco.png\">"});
    str = str.replace(/click here/gi, function myReplaceFunction(x) { return "<span style=\"color: blue; text-decoration: underline;\">Click here</span>"});

    return str;
}