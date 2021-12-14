layui.use(['layer','util'],function(){
	var $ = layui.$;
	//图片放大处理
	$(".layer-photos").each(function(index,elem){
		layer.photos({
			photos:this,
			anim:5//0-6的选择，指定弹出图片动画类型，默认随机（请注意，3.0之前的版本用shift参数）
		}); 
	});
	//右下角返回顶部按钮浮框
	layui.util.fixbar({
		bar1:'&#xe615;',
		click: function(type){
			if(type === 'bar1'){
				openSiteDir();
			}
		}
	});
	
	function openSiteDir(){
		var siteDir = $('.site-dir');
		if(!siteDir[0]){
			//目录-锚点导航
			var anchorList = $(".editor-anchor"),
				anchorHtml = [];
				
			anchorList.each(function(index,elem){
				anchorHtml.push('<li><a href="#' + $(this).attr("name") + '"><cite>' + $(this).attr("anchor-name") + '</cite></a></li>');
			});
			if(anchorList.length > 0){
				anchorHtml.unshift('<ul class="site-dir">');
				anchorHtml.push('</ul>');
			}
			if(anchorHtml.length > 0){
				$("body").append(anchorHtml.join(''));
			}
			siteDir = $('.site-dir');
		}
		
		if(siteDir[0]){// && $(window).width() > 750
			layer.ready(function(){
				layer.open({
					type:1
					,content:siteDir
					,skin:'layui-layer-dir'
					,area:'auto'
					,maxHeight:$(window).height() - 300
					,title:'目录'
					//,closeBtn: false
					,offset:'r'
					,shade: false
					,success: function(layero, index){
						layer.style(index, {
							marginLeft: -15
						});
					}
				});
			});
			siteDir.find('li').on('click', function(){
				var othis = $(this);
				othis.find('a').addClass('layui-this');
				othis.siblings().find('a').removeClass('layui-this');
			});
		}
	}
	
	openSiteDir();
});