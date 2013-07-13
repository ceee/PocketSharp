/*! wingsforlife 2013-07-13 */
(function(e,t,a,n,r,i,s){e.GoogleAnalyticsObject=r,e[r]=e[r]||function(){(e[r].q=e[r].q||[]).push(arguments)},e[r].l=1*new Date,i=t.createElement(a),s=t.getElementsByTagName(a)[0],i.async=1,i.src=n,s.parentNode.insertBefore(i,s)})(window,document,"script","//www.google-analytics.com/analytics.js","ga"),ga("create","UA-41313133-2","frontendplay.com"),ga("send","pageview"),function(){function e(e){if(e&&e.preventDefault(),this.getAttribute){for(var a=this.getAttribute("data-id"),n=document.querySelectorAll("div[data-part]"),r=!1,i=n.length-1;i>=0;i--)r=n[i].getAttribute("data-part")===a,n[i].style.display=r?"block":"none",t(this);window.location.hash=a}}function t(e){for(var t=a.length-1;t>=0;t--)a[t].classList.remove("is-active");e.classList.add("is-active")}var a=document.querySelectorAll(".app-nav a");window.location.hash.length>1&&(console.info(window.location.hash),e.call(document.querySelector('a[data-id="'+window.location.hash.slice(1)+'"]')));for(var n=a.length-1;n>=0;n--)a[n].addEventListener("click",e,!1)}(),function(){var e=/\blang(?:uage)?-(?!\*)(\w+)\b/i,t=self.Prism={util:{type:function(e){return Object.prototype.toString.call(e).match(/\[object (\w+)\]/)[1]},clone:function(e){var a=t.util.type(e);switch(a){case"Object":var n={};for(var r in e)e.hasOwnProperty(r)&&(n[r]=t.util.clone(e[r]));return n;case"Array":return e.slice()}return e}},languages:{extend:function(e,a){var n=t.util.clone(t.languages[e]);for(var r in a)n[r]=a[r];return n},insertBefore:function(e,a,n,r){r=r||t.languages;var i=r[e],s={};for(var l in i)if(i.hasOwnProperty(l)){if(l==a)for(var o in n)n.hasOwnProperty(o)&&(s[o]=n[o]);s[l]=i[l]}return r[e]=s},DFS:function(e,a){for(var n in e)a.call(e,n,e[n]),"Object"===t.util.type(e)&&t.languages.DFS(e[n],a)}},highlightAll:function(e,a){for(var n,r=document.querySelectorAll('code[class*="language-"], [class*="language-"] code, code[class*="lang-"], [class*="lang-"] code'),i=0;n=r[i++];)t.highlightElement(n,e===!0,a)},highlightElement:function(n,r,i){for(var s,l,o=n;o&&!e.test(o.className);)o=o.parentNode;if(o&&(s=(o.className.match(e)||[,""])[1],l=t.languages[s]),l){n.className=n.className.replace(e,"").replace(/\s+/g," ")+" language-"+s,o=n.parentNode,/pre/i.test(o.nodeName)&&(o.className=o.className.replace(e,"").replace(/\s+/g," ")+" language-"+s);var g=n.textContent;if(g){g=g.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\u00a0/g," ");var c={element:n,language:s,grammar:l,code:g};if(t.hooks.run("before-highlight",c),r&&self.Worker){var u=new Worker(t.filename);u.onmessage=function(e){c.highlightedCode=a.stringify(JSON.parse(e.data)),c.element.innerHTML=c.highlightedCode,i&&i.call(c.element),t.hooks.run("after-highlight",c)},u.postMessage(JSON.stringify({language:c.language,code:c.code}))}else c.highlightedCode=t.highlight(c.code,c.grammar),c.element.innerHTML=c.highlightedCode,i&&i.call(n),t.hooks.run("after-highlight",c)}}},highlight:function(e,n){return a.stringify(t.tokenize(e,n))},tokenize:function(e,a){var n=t.Token,r=[e],i=a.rest;if(i){for(var s in i)a[s]=i[s];delete a.rest}e:for(var s in a)if(a.hasOwnProperty(s)&&a[s]){var l=a[s],o=l.inside,g=!!l.lookbehind||0;l=l.pattern||l;for(var c=0;r.length>c;c++){var u=r[c];if(r.length>e.length)break e;if(!(u instanceof n)){l.lastIndex=0;var p=l.exec(u);if(p){g&&(g=p[1].length);var f=p.index-1+g,p=p[0].slice(g),d=p.length,m=f+d,h=u.slice(0,f+1),y=u.slice(m+1),w=[c,1];h&&w.push(h);var v=new n(s,o?t.tokenize(p,o):p);w.push(v),y&&w.push(y),Array.prototype.splice.apply(r,w)}}}}return r},hooks:{all:{},add:function(e,a){var n=t.hooks.all;n[e]=n[e]||[],n[e].push(a)},run:function(e,a){var n=t.hooks.all[e];if(n&&n.length)for(var r,i=0;r=n[i++];)r(a)}}},a=t.Token=function(e,t){this.type=e,this.content=t};if(a.stringify=function(e){if("string"==typeof e)return e;if("[object Array]"==Object.prototype.toString.call(e))return e.map(a.stringify).join("");var n={type:e.type,content:a.stringify(e.content),tag:"span",classes:["token",e.type],attributes:{}};"comment"==n.type&&(n.attributes.spellcheck="true"),t.hooks.run("wrap",n);var r="";for(var i in n.attributes)r+=i+'="'+(n.attributes[i]||"")+'"';return"<"+n.tag+' class="'+n.classes.join(" ")+'" '+r+">"+n.content+"</"+n.tag+">"},!self.document)return self.addEventListener("message",function(e){var a=JSON.parse(e.data),n=a.language,r=a.code;self.postMessage(JSON.stringify(t.tokenize(r,t.languages[n]))),self.close()},!1),void 0;var n=document.getElementsByTagName("script");n=n[n.length-1],n&&(t.filename=n.src,document.addEventListener&&!n.hasAttribute("data-manual")&&document.addEventListener("DOMContentLoaded",t.highlightAll))}(),Prism.languages.markup={comment:/&lt;!--[\w\W]*?--(&gt;|&gt;)/g,prolog:/&lt;\?.+?\?&gt;/,doctype:/&lt;!DOCTYPE.+?&gt;/,cdata:/&lt;!\[CDATA\[[\w\W]*?]]&gt;/i,tag:{pattern:/&lt;\/?[\w:-]+\s*(?:\s+[\w:-]+(?:=(?:("|')(\\?[\w\W])*?\1|\w+))?\s*)*\/?&gt;/gi,inside:{tag:{pattern:/^&lt;\/?[\w:-]+/i,inside:{punctuation:/^&lt;\/?/,namespace:/^[\w-]+?:/}},"attr-value":{pattern:/=(?:('|")[\w\W]*?(\1)|[^\s>]+)/gi,inside:{punctuation:/=|&gt;|"/g}},punctuation:/\/?&gt;/g,"attr-name":{pattern:/[\w:-]+/g,inside:{namespace:/^[\w-]+?:/}}}},entity:/&amp;#?[\da-z]{1,8};/gi},Prism.hooks.add("wrap",function(e){"entity"===e.type&&(e.attributes.title=e.content.replace(/&amp;/,"&"))}),Prism.languages.css={comment:/\/\*[\w\W]*?\*\//g,atrule:/@[\w-]+?(\s+[^;{]+)?(?=\s*{|\s*;)/gi,url:/url\((["']?).*?\1\)/gi,selector:/[^\{\}\s][^\{\}]*(?=\s*\{)/g,property:/(\b|\B)[a-z-]+(?=\s*:)/gi,string:/("|')(\\?.)*?\1/g,important:/\B!important\b/gi,ignore:/&(lt|gt|amp);/gi,punctuation:/[\{\};:]/g},Prism.languages.markup&&Prism.languages.insertBefore("markup","tag",{style:{pattern:/(&lt;|<)style[\w\W]*?(>|&gt;)[\w\W]*?(&lt;|<)\/style(>|&gt;)/gi,inside:{tag:{pattern:/(&lt;|<)style[\w\W]*?(>|&gt;)|(&lt;|<)\/style(>|&gt;)/gi,inside:Prism.languages.markup.tag.inside},rest:Prism.languages.css}}}),Prism.languages.clike={comment:{pattern:/(^|[^\\])(\/\*[\w\W]*?\*\/|\/\/.*?(\r?\n|$))/g,lookbehind:!0},string:/("|')(\\?.)*?\1/g,keyword:/\b(if|else|while|do|for|return|in|instanceof|function|new|try|catch|finally|null|break|continue)\b/g,"boolean":/\b(true|false)\b/g,number:/\b-?(0x)?\d*\.?[\da-f]+\b/g,operator:/[-+]{1,2}|!|=?&lt;|=?&gt;|={1,2}|(&amp;){1,2}|\|?\||\?|\*|\//g,ignore:/&(lt|gt|amp);/gi,punctuation:/[{}[\];(),.:]/g},Prism.languages.javascript=Prism.languages.extend("clike",{keyword:/\b(var|let|if|else|while|do|for|return|in|instanceof|function|new|with|typeof|try|catch|finally|null|break|continue)\b/g,number:/\b(-?(0x)?\d*\.?[\da-f]+|NaN|-?Infinity)\b/g}),Prism.languages.insertBefore("javascript","keyword",{regex:{pattern:/(^|[^/])\/(?!\/)(\[.+?]|\\.|[^/\r\n])+\/[gim]{0,3}(?=\s*($|[\r\n,.;})]))/g,lookbehind:!0}}),Prism.languages.markup&&Prism.languages.insertBefore("markup","tag",{script:{pattern:/(&lt;|<)script[\w\W]*?(>|&gt;)[\w\W]*?(&lt;|<)\/script(>|&gt;)/gi,inside:{tag:{pattern:/(&lt;|<)script[\w\W]*?(>|&gt;)|(&lt;|<)\/script(>|&gt;)/gi,inside:Prism.languages.markup.tag.inside},rest:Prism.languages.javascript}}}),Prism.languages.coffeescript=Prism.languages.extend("javascript",{"block-comment":/([#]{3}\s*\r?\n(.*\s*\r*\n*)\s*?\r?\n[#]{3})/g,comment:/(\s|^)([#]{1}[^#^\r^\n]{2,}?(\r?\n|$))/g,keyword:/\b(this|window|delete|class|extends|namespace|extend|ar|let|if|else|while|do|for|each|of|return|in|instanceof|new|with|typeof|try|catch|finally|null|undefined|break|continue)\b/g}),Prism.languages.insertBefore("coffeescript","keyword",{"function":{pattern:/[a-z|A-z]+\s*[:|=]\s*(\([.|a-z\s|,|:|{|}|\"|\'|=]*\))?\s*-&gt;/gi,inside:{"function-name":/[_?a-z-|A-Z-]+(\s*[:|=])| @[_?$?a-z-|A-Z-]+(\s*)| /g,operator:/[-+]{1,2}|!|=?&lt;|=?&gt;|={1,2}|(&amp;){1,2}|\|?\||\?|\*|\//g}},"class-name":{pattern:/(class\s+)[a-z-]+[\.a-z]*\s/gi,lookbehind:!0},"attr-name":/[_?a-z-|A-Z-]+(\s*:)| @[_?$?a-z-|A-Z-]+(\s*)| /g});