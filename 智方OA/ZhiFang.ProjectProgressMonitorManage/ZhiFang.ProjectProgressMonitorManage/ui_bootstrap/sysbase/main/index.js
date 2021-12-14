$(function() {
	$("#Info").on("click",function(){
		addTab('messageinfo','消息管理','../../building.html');
		hideTipNumber();
	});
	$("#Close").on("click",function(){
		fh.confirm('您真的要退出吗？',function(){
			if($('#exitFrame').size() > 0){
				$('#exitFrame').attr('src','/portal/logonOff.action?refer=' + encodeURIComponent('https%3A%2F%2Fcloud.waiqin365.com'));
			}else{
				$('<iframe id="exitFrame" src="/portal/logonOff.action?refer=' + encodeURIComponent('https%3A%2F%2Fcloud.waiqin365.com') + '" style="display:none;"></iframe>').appendTo('body');
			}
    	});
	});
	
	function showTipNumer(n){
		$('#message_number').html('(' + n + ')');
	    $('#message_number').css('display','');
	}
	    
	function hideTipNumber(){
	 	$('#message_number').html('');
	    $('#message_number').css('display','none');
	}
	
	function addTab(id,title,url,closable){
	    if ($('#tabs').tabs('existsById', 'tab' + id)){
	        var tab = $('#tabs').tabs('getTabById','tab' + id);
	        $('#tabs').tabs('selectById', 'tab' + id);
	        $('#tabs').tabs('update',{tab:tab,options:{title:title}});
	    }else{
	    	var tl = $('#'+id).attr('node-tab');
	        if(tl){
		        var info = [], json = eval('(' + tl + ')');
	            info.push('<div class="easyui-tabs" data-options="fit:true,border:false,onSelect:recordChildTab">');
	            	for(var i = 0; i < json.length; i ++){
	            		var u = json[i].url;
	            		info.push('<div id="tab' + json[i].id + '" title="' + json[i].name + '" cls="' + u + '"></div>');
	            	}
	            info.push('</div>');
	            
	            $('#tabs').tabs('add',{
		        	id:'tab' + id,
		            title:title,
		            content:info.join(''),
		            closable: (closable == undefined) 
		        });
	        }else{
	        	if(url.indexOf('http') != 0){
	        		if(url.indexOf('/') != 0){
	        			url = '/' + url;
	        		}
	        	}
	        	var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
	        	
		        $('#tabs').tabs('add',{
		        	id:'tab' + id,
		            title:title,
		            content:content,
		            closable: (closable == undefined) 
		        });
	        }
	    }
	    tabContext();
		tabContextEvent();
	}
	//页面大小变化
	function resizePage(ele){
		if($(ele).hasClass('layout-button-left')){
			var layout = $('#layout');
        	var west = layout.layout('panel','west');
        	$('.menu-wrap').hide();
        	west.animate({width:'72'},function(){
        		west.panel('resize', {width:'72'});
        		layout.layout('resize');
        		$('#menu .panel .menu-switch span').removeClass('layout-button-left').addClass('layout-button-right');
        	});
		}else{
			var layout = $('#layout');
        	var west = layout.layout('panel','west');
        	west.panel('resize', {width:'203'});
       		layout.layout('resize');
       		$('.menu-wrap').show();
       		$('#menu .panel .menu-switch span').removeClass('layout-button-right').addClass('layout-button-left');
		}
	}
	//用户信息
	$('.user-name').tooltip({
        content: function(){
        	var html = [];
        	html.push('<div class="user-info">');
        	html.push('<span class="li"><a href="javascript:toSelfInfo();">个人信息</a></span>');
        	html.push('<span class="li"><a href="javascript:toPassword();">修改密码</a></span>');
        	html.push('</div>');       	                	
            return html.join('');
        },
        onShow: function(){
            var t = $(this);
            t.tooltip('tip').css({
            	'width':t.width() + 76 + 'px',
            	'left':t.offset().left + 'px'
            }).unbind().bind('mouseenter', function(){
                t.addClass('has-pulldown').tooltip('show');
            }).bind('mouseleave', function(){
                t.removeClass('has-pulldown').tooltip('hide');
            });
        }
    }).hover(function(){
    	$(this).addClass('has-pulldown');
    },function(){
    	$(this).removeClass('has-pulldown');
    });
});