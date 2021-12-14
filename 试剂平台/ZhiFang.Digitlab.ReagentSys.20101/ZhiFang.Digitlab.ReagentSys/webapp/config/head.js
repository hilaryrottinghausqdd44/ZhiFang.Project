(function() {
	//-----------系统名称-----------
	var content = [];
	//content.push('<meta charset="utf-8">');
	content.push('<meta http-equiv="X-UA-Compatible" content="IE=edge">');
	content.push('<meta name="viewport" content="width=device-width,initial-scale=1, minimum-scale=1.0" />');
	content.push('<meta name="description" content="">');
	content.push('<meta name="author" content="">');
	content.push('<meta name="renderer" content="webkit">');
	//uc强制竖屏
	content.push('<meta name="screen-orientation" content="portrait">');
	//QQ强制竖屏
	content.push('<meta name="x5-orientation" content="portrait">');
	//UC强制全屏
	content.push('<meta name="full-screen" content="yes">');
	//QQ强制全屏
	content.push('<meta name="x5-fullscreen" content="true">');
	//UC应用模式
	content.push('<meta name="browsermode" content="application">');
	//QQ应用模式
	content.push('<meta name="x5-page-mode" content="app">');
	//windows phone 点击无高光
	content.push('<meta name="msapplication-tap-highlight" content="no">');
	//适应移动端end
	content.push('<link rel="shortcut icon" href="' + Shell.util.Path.UI + '/img/logo-16.png">');
	
	var css = [];
	css.push('/src/bootstrap-3.3.2-dist/css/bootstrap.css');
	css.push('/css/css.css');
	css.push('/css/login.css');
	
	var cssLen = css.length;
	if(cssLen > 0){
		for(var i=0;i<cssLen;i++){
			css[i] = '<link rel="stylesheet" type="text/css" href="' + 
				Shell.util.Path.UI + css[i] + '">';
		}
		content.push(css.join(""));
	}
	
	//写入当前页面中
	var len = content.length;
	if(len > 0){
		document.write(content.join(''));
	}
})();