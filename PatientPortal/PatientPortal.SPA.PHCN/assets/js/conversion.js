(function(){var h=this,l=function(a){return"string"==typeof a},aa=Date.now||function(){return+new Date};var m=function(a){a=parseFloat(a);return isNaN(a)||1<a||0>a?0:a};var ba=m("0.20"),ca=m("0.10"),da=m("0.0");var ea=/^true$/.test("false")?!0:!1;var fa=Array.prototype.indexOf?function(a,b,c){return Array.prototype.indexOf.call(a,b,c)}:function(a,b,c){c=null==c?0:0>c?Math.max(0,a.length+c):c;if(l(a))return l(b)&&1==b.length?a.indexOf(b,c):-1;for(;c<a.length;c++)if(c in a&&a[c]===b)return c;return-1},ga=Array.prototype.filter?function(a,b,c){return Array.prototype.filter.call(a,b,c)}:function(a,b,c){for(var d=a.length,e=[],f=0,g=l(a)?a.split(""):a,k=0;k<d;k++)if(k in g){var n=g[k];b.call(c,n,k,a)&&(e[f++]=n)}return e},ha=Array.prototype.map?
function(a,b,c){return Array.prototype.map.call(a,b,c)}:function(a,b,c){for(var d=a.length,e=Array(d),f=l(a)?a.split(""):a,g=0;g<d;g++)g in f&&(e[g]=b.call(c,f[g],g,a));return e},ia=function(a){return Array.prototype.concat.apply([],arguments)};var p=function(a){var b=[],c=0,d;for(d in a)b[c++]=a[d];return b};var r=function(a){r[" "](a);return a};r[" "]=function(){};var v=function(a,b){for(var c in a)Object.prototype.hasOwnProperty.call(a,c)&&b.call(void 0,a[c],c,a)};var ka=function(){var a=p(ja);this.i={};this.b={};for(var a=a||[],b=0,c=a.length;b<c;++b)this.b[a[b]]=""},w=function(){try{var a=h.top.location.hash;if(a){var b=a.match(/\bdeid=([\d,]+)/);return b&&b[1]||""}}catch(c){}return""},A=function(a,b,c){var d=y;if(c?d.b.hasOwnProperty(c)&&""==d.b[c]:1){var e;if(e=(e=w().match(new RegExp("\\b("+a.join("|")+")\\b")))&&e[0]||null)a=e;else a:{if(!(1E-4>Math.random())&&(e=Math.random(),e<b)){try{var f=new Uint32Array(1);h.crypto.getRandomValues(f);e=f[0]/65536/
65536}catch(g){e=Math.random()}a=a[Math.floor(e*a.length)];break a}a=null}a&&""!=a&&(c?d.b.hasOwnProperty(c)&&(d.b[c]=a):d.i[a]=!0)}},B=function(a){var b=y;return b.b.hasOwnProperty(a)?b.b[a]:""},la=function(){var a=y,b=[];v(a.i,function(a,d){b.push(d)});v(a.b,function(a){""!=a&&b.push(a)});return b};var ja={f:2,g:3,h:4},C={g:{c:"27391101",a:"27391102"},f:{c:"376635470",a:"376635471"},h:{c:"659234980",a:"659234981"}},y=null,ma=function(){var a=ia.apply([],ha(p(C),function(a){return p(a)},void 0)),b=ga(w().split(","),function(b){return""!=b&&!(0<=fa(a,b))});return 0<b.length?"&debug_experiment_id="+b.join(","):""};var D=function(a,b,c){for(;0<=(b=a.indexOf("fmt",b))&&b<c;){var d=a.charCodeAt(b-1);if(38==d||63==d)if(d=a.charCodeAt(b+3),!d||61==d||38==d||35==d)return b;b+=4}return-1},E=/#|$/,na=/[?&]($|#)/;var F="google_conversion_id google_conversion_format google_conversion_type google_conversion_order_id google_conversion_language google_conversion_value google_conversion_evaluemrc google_conversion_currency google_conversion_domain google_conversion_label google_conversion_color google_disable_viewthrough google_enable_display_cookie_match google_remarketing_only google_remarketing_for_search google_conversion_items google_conversion_merchant_id google_custom_params google_conversion_date google_conversion_time google_conversion_js_version onload_callback opt_image_generator google_conversion_page_url google_conversion_referrer_url".split(" "),
G=["google_conversion_first_time","google_conversion_snippets"],H=function(a){return null!=a?encodeURIComponent(a.toString()):""},J=function(a){return null!=a?a.toString().substring(0,512):""},K=function(a,b){b=H(b);return""!=b&&(a=H(a),""!=a)?"&".concat(a,"=",b):""},L=function(a){var b=typeof a;return null==a||"object"==b||"function"==b?null:String(a).replace(/,/g,"\\,").replace(/;/g,"\\;").replace(/=/g,"\\=")},oa=function(a){if((a=a.google_custom_params)&&"object"==typeof a&&"function"!=typeof a.join){var b=
[];for(g in a)if(Object.prototype.hasOwnProperty.call(a,g)){var c=a[g];if(c&&"function"==typeof c.join){for(var d=[],e=0;e<c.length;++e){var f=L(c[e]);null!=f&&d.push(f)}c=d.length?d.join(","):null}else c=L(c);(d=L(g))&&null!=c&&b.push(d+"="+c)}var g=b.join(";")}else g="";return""==g?"":"&".concat("data=",encodeURIComponent(g))},pa=function(a){if(null!=a){a=a.toString();if(2==a.length)return K("hl",a);if(5==a.length)return K("hl",a.substring(0,2))+K("gl",a.substring(3,5))}return""};
function M(a){return"number"!=typeof a&&"string"!=typeof a?"":H(a.toString())}
var qa=function(a){if(!a)return"";a=a.google_conversion_items;if(!a)return"";for(var b=[],c=0,d=a.length;c<d;c++){var e=a[c],f=[];e&&(f.push(M(e.value)),f.push(M(e.quantity)),f.push(M(e.item_id)),f.push(M(e.adwords_grouping)),f.push(M(e.sku)),b.push("("+f.join("*")+")"))}return 0<b.length?"&item="+b.join(""):""},ra=function(a,b,c){var d=[];if(a){var e=a.screen;e&&(d.push(K("u_h",e.height)),d.push(K("u_w",e.width)),d.push(K("u_ah",e.availHeight)),d.push(K("u_aw",e.availWidth)),d.push(K("u_cd",e.colorDepth)));
a.history&&d.push(K("u_his",a.history.length))}c&&"function"==typeof c.getTimezoneOffset&&d.push(K("u_tz",-c.getTimezoneOffset()));b&&("function"==typeof b.javaEnabled&&d.push(K("u_java",b.javaEnabled())),b.plugins&&d.push(K("u_nplug",b.plugins.length)),b.mimeTypes&&d.push(K("u_nmime",b.mimeTypes.length)));return d.join("")};
function sa(a){a=a?a.title:"";if(void 0==a||""==a)return"";var b=function(a){try{return decodeURIComponent(a),!0}catch(e){return!1}};a=encodeURIComponent(a);for(var c=256;!b(a.substr(0,c));)c--;return"&tiba="+a.substr(0,c)}
var N=function(a,b,c,d){var e="";if(b){if(a.top==a)var f=0;else{var g=a.location.ancestorOrigins;if(g)f=g[g.length-1]==a.location.origin?1:2;else{g=a.top;try{var k;if(k=!!g&&null!=g.location.href)c:{try{r(g.foo);k=!0;break c}catch(n){}k=!1}f=k}catch(n){f=!1}f=f?1:2}}a=c?c:1==f?a.top.location.href:a.location.href;e+=K("frm",f);e+=K("url",J(a));e+=K("ref",J(d||b.referrer))}return e},O=function(a,b){return!(ea||b&&ta.test(navigator.userAgent))||a&&a.location&&a.location.protocol&&"https:"==a.location.protocol.toString().toLowerCase()?
"https:":"http:"},P=function(a,b,c){c=c.google_remarketing_only?"googleads.g.doubleclick.net":c.google_conversion_domain||"www.googleadservices.com";return O(a,/www[.]googleadservices[.]com/i.test(c))+"//"+c+"/pagead/"+b},ua=function(a,b,c,d){var e="/?";"landing"==d.google_conversion_type&&(e="/extclk?");e=[d.google_remarketing_only?"viewthroughconversion/":"conversion/",H(d.google_conversion_id),e,"random=",H(d.google_conversion_time)].join("");e=P(a,e,d);a=[K("cv",d.google_conversion_js_version),
K("fst",d.google_conversion_first_time),K("num",d.google_conversion_snippets),K("fmt",d.google_conversion_format),K("value",d.google_conversion_value),K("evaluemrc",d.google_conversion_evaluemrc),K("currency_code",d.google_conversion_currency),K("label",d.google_conversion_label),K("oid",d.google_conversion_order_id),K("bg",d.google_conversion_color),pa(d.google_conversion_language),K("guid","ON"),K("disvt",d.google_disable_viewthrough),K("eid",la().join()),qa(d),ra(a,b,d.google_conversion_date),
oa(d),N(a,c,d.google_conversion_page_url,d.google_conversion_referrer_url),d.google_remarketing_for_search&&!d.google_conversion_domain?"&srr=n":"",sa(c)].join("")+ma();return e+a},Q=function(a,b,c,d,e,f){return'<iframe name="'+a+'" title="'+b+'" width="'+d+'" height="'+e+'" src="'+c+'" frameborder="0" marginwidth="0" marginheight="0" vspace="0" hspace="0" allowtransparency="true"'+(f?' style="display:none"':"")+' scrolling="no"></iframe>'},va=function(a){return{ar:1,bg:1,cs:1,da:1,de:1,el:1,en_AU:1,
en_US:1,en_GB:1,es:1,et:1,fi:1,fr:1,hi:1,hr:1,hu:1,id:1,is:1,it:1,iw:1,ja:1,ko:1,lt:1,nl:1,no:1,pl:1,pt_BR:1,pt_PT:1,ro:1,ru:1,sk:1,sl:1,sr:1,sv:1,th:1,tl:1,tr:1,vi:1,zh_CN:1,zh_TW:1}[a]?a+".html":"en_US.html"},ta=/Android ([01]\.|2\.[01])/i,wa=function(a,b){if(!b.google_remarketing_only||!b.google_enable_display_cookie_match||!y||B(2)!=C.f.a)return"";a=O(a,!1)+"//bid.g.doubleclick.net/xbbe/pixel?d=KAE";return Q("google_cookie_match_frame","Google cookie match frame",a,1,1,!0)};
function R(a,b){return'<img height="1" width="1" border="0" alt="" src="'+a.replace("gen204Type",b)+'" style="display:none" />'}
var xa=function(a){if(y&&B(4)==C.h.a){var b=Math.floor(1E9*Math.random()),c=O(a,!1)+"//pagead2.googlesyndication.com/pagead/gen_204?id=beacon-api-web-survey&type=gen204Type",c=c+K("random",b),c=c+K("time",aa());if(a.navigator&&a.navigator.sendBeacon){try{var d=c.replace("gen204Type","send-beacon");a.navigator.sendBeacon(d,"")}catch(e){}return R(c,"img")}return R(c,"beacon-undefined")}return""},S=function(a,b,c,d){var e="";d.google_remarketing_only&&d.google_enable_display_cookie_match&&(y&&A([C.f.c,
C.f.a],ba,2),e=wa(a,d));3!=d.google_conversion_format||d.google_remarketing_only||d.google_conversion_domain||y&&A([C.g.c,C.g.a],ca,3);y&&A([C.h.c,C.h.a],da,4);b=ua(a,b,c,d);var f=function(a,b,c,d){return'<img height="'+c+'" width="'+b+'" border="0" alt="" src="'+a+'"'+(d?' style="display:none"':"")+" />"};return 0==d.google_conversion_format&&null==d.google_conversion_domain?'<a href="'+(O(a,!1)+"//services.google.com/sitestats/"+va(d.google_conversion_language)+"?cid="+H(d.google_conversion_id))+
'" target="_blank">'+f(b,135,27,!1)+"</a>"+e:1<d.google_conversion_snippets||3==d.google_conversion_format?ya(c,b)?e:f(b,1,1,!0)+e+xa(a):Q("google_conversion_frame","Google conversion frame",b,2==d.google_conversion_format?200:300,2==d.google_conversion_format?26:13,!1)+e};function za(){return new Image}
function ya(a,b){if(y&&B(3)===C.g.a)try{var c=b.search(E),d=D(b,0,c);if(0>d)var e=null;else{var f=b.indexOf("&",d);if(0>f||f>c)f=c;d+=4;e=decodeURIComponent(b.substr(d,f-d).replace(/\+/g," "))}if(3!=e)var g=!1;else{var k=b.search(E);e=0;for(var n,c=[];0<=(n=D(b,e,k));)c.push(b.substring(e,n)),e=Math.min(b.indexOf("&",n)+1||k,k);c.push(b.substr(e));var q=c.join("").replace(na,"$1");var z="fmt="+encodeURIComponent("4");if(z){var t=q.indexOf("#");0>t&&(t=q.length);var x=q.indexOf("?");if(0>x||x>t){x=
t;var T=""}else T=q.substring(x+1,t);var u=[q.substr(0,x),T,q.substr(t)];var I=u[1];u[1]=z?I?I+"&"+z:z:I;var U=u[0]+(u[1]?"?"+u[1]:"")+u[2]}else U=q;var V=a.createElement("script");V.src=U;a.getElementsByTagName("script")[0].parentElement.appendChild(V);g=!0}return g}catch(Ja){}return!1}
var Aa=function(a,b){var c=a.opt_image_generator&&a.opt_image_generator.call;b+=K("async","1");var d=za;c&&(d=a.opt_image_generator);a=d();a.src=b;a.onload=function(){}},W=function(a,b,c){var d=Math.floor(1E9*Math.random()),d=[H(c.google_conversion_id),"/?random=",d].join(""),d=O(a,!1)+"//www.google.com/ads/user-lists/"+d;a=[K("label",c.google_conversion_label),K("fmt","3"),N(a,b,c.google_conversion_page_url,c.google_conversion_referrer_url)].join("");Aa(c,d+a)},Ba=function(a,b){for(var c=document.createElement("iframe"),
d=[],e=[],f=0;f<b.google_conversion_items.length;f++){var g=b.google_conversion_items[f];g&&g.quantity&&g.sku&&(d.push(g.sku),e.push(g.quantity))}a=O(a,!1)+"//www.google.com/ads/mrc";c.src=a+"?sku="+d.join(",")+"&qty="+e.join(",")+"&oid="+b.google_conversion_order_id+"&mcid="+b.google_conversion_merchant_id;c.style.width="1px";c.style.height="1px";c.style.display="none";return c},Ca=function(a){if("landing"==a.google_conversion_type||!a.google_conversion_id||a.google_remarketing_only&&a.google_disable_viewthrough)return!1;
a.google_conversion_date=new Date;a.google_conversion_time=a.google_conversion_date.getTime();a.google_conversion_snippets="number"==typeof a.google_conversion_snippets&&0<a.google_conversion_snippets?a.google_conversion_snippets+1:1;"number"!=typeof a.google_conversion_first_time&&(a.google_conversion_first_time=a.google_conversion_time);a.google_conversion_js_version="8";0!=a.google_conversion_format&&1!=a.google_conversion_format&&2!=a.google_conversion_format&&3!=a.google_conversion_format&&(a.google_conversion_format=
1);!1!==a.google_enable_display_cookie_match&&(a.google_enable_display_cookie_match=!0);y=new ka;return!0},Da=function(a){for(var b=0;b<F.length;b++)a[F[b]]=null},Ea=function(a){for(var b={},c=0;c<F.length;c++)b[F[c]]=a[F[c]];for(c=0;c<G.length;c++)b[G[c]]=a[G[c]];return b},Fa=function(a){var b=document.getElementsByTagName("head")[0];b||(b=document.createElement("head"),document.getElementsByTagName("html")[0].insertBefore(b,document.getElementsByTagName("body")[0]));var c=document.createElement("script");
c.src=P(window,"conversion_debug_overlay.js",a);b.appendChild(c)};var X=function(){var a=!1;try{var b=Object.defineProperty({},"passive",{get:function(){a=!0}});h.addEventListener("test",null,b)}catch(c){}return a}(),Ga=function(a,b,c){a.addEventListener?a.addEventListener(b,c,X?void 0:!1):a.attachEvent&&a.attachEvent("on"+b,c)};var Y=function(a){return{visible:1,hidden:2,prerender:3,preview:4}[a.webkitVisibilityState||a.mozVisibilityState||a.visibilityState||""]||0},Ha=function(a){var b;a.mozVisibilityState?b="mozvisibilitychange":a.webkitVisibilityState?b="webkitvisibilitychange":a.visibilityState&&(b="visibilitychange");return b},Z=function(a,b){if(3==Y(b))return!1;a();return!0},Ia=function(a,b){if(!Z(a,b)){var c=!1,d=Ha(b),e=function(){if(!c&&Z(a,b)){c=!0;var f=e;b.removeEventListener?b.removeEventListener(d,f,X?void 0:
!1):b.detachEvent&&b.detachEvent("on"+d,f)}};d&&Ga(b,d,e)}};(function(a,b,c){if(a)if(/[\?&;]google_debug/.exec(document.URL))Fa(a);else{try{if(Ca(a))if(3==Y(c)){var d=Ea(a),e="google_conversion_"+Math.floor(1E9*Math.random());c.write('<span id="'+e+'"></span>');Ia(function(){try{var f=c.getElementById(e);f&&(f.innerHTML=S(a,b,c,d),d.google_remarketing_for_search&&!d.google_conversion_domain&&W(a,c,d))}catch(g){}},c)}else c.write(S(a,b,c,a)),a.google_remarketing_for_search&&!a.google_conversion_domain&&W(a,c,a);a.google_conversion_merchant_id&&a.google_conversion_order_id&&
a.google_conversion_items&&c.documentElement.appendChild(Ba(a,a))}catch(f){}Da(a)}})(window,navigator,document);}).call(this);
