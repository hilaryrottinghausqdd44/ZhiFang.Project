$(function(){
	var CARDS = {};
	//卡片信息
	var CARD_INFO_OBJ = {
		task:{id:'task',title:'任务事项',row:1,colCount:12,name:'task',more:'/task/index.html'},
		task1:{id:'task1',title:'任务事项1',row:2,colCount:6,name:'task',more:'/task1/index.html'},
		task2:{id:'task2',title:'任务事项2',row:2,colCount:6,name:'task',more:'/task2/index.html'},
		task3:{id:'task3',title:'任务事项3',row:3,colCount:4,name:'task',more:'/task3/index.html'},
		task4:{id:'task4',title:'任务事项4',row:3,colCount:4,name:'task',more:'/task4/index.html'},
		task5:{id:'task5',title:'任务事项5',row:3,colCount:4,name:'task',more:'http://www.baidu.com'},
	};
	//初始化卡片
	function initCrads(list){
		var rows = {},
			html = [];
			
		for(var i in CARD_INFO_OBJ){
			var info = CARD_INFO_OBJ[i];
			if(!rows[info.row]){
				rows[info.row] = [];
			}
			rows[info.row].push(createCardHtml(info));
		}
		
		for(var i in rows){
			html.push('<div class="row">');
			html.push(rows[i].join(""));
			html.push('</div>');
		}
		//初始化卡片框架
		$("#main-page").html(html.join(""));
		//初始化卡片监听
		initListeners();
		//加载开篇内容
		for(var i in CARD_INFO_OBJ){
			loadCardContent(CARD_INFO_OBJ[i]);
		}
	}
	//创建卡片内容
	function createCardHtml(info){
		var html = 
		'<div id="{Id}" class="col-sm-{ColCount}">'
			+'<div id="{Id}-div" class="ibox float-e-margins">'
                +'<div class="ibox-title">'
					+'<h5 id="{Id}-title">{Title}</h5>'
					+'<div class="ibox-tools">'
						+'<a class="collapse-link">'
							+'<i class="fa fa-chevron-up"></i>'
						+'</a>'
						+'<a class="collapse-link">'
							+'<i class="fa fa-refresh"></i>'
						+'</a>'
						+'<a class="collapse-link J_menuItem" href="{More}" data-index="{Index}">'
							+'<i class="fa fa-search"></i><span hidden="hidden">{Title}</span>'
						+'</a>'
					+'</div>'
				+'</div>'
				+'<div id="{Id}-content" class="ibox-content">'
				+'</div>'
			+'</div>'
		+'</div>';
		
		html = html.replace(/{Id}/g,info.id);
		html = html.replace(/{ColCount}/g,info.colCount);
		html = html.replace(/{Title}/g,info.title);
		html = html.replace(/{More}/g,JcallShell.System.Path.getUiUrl(info.more));
		html = html.replace(/{Index}/g,new Date().getTime());
		
		return html;
	}
	//初始化卡片监听
	function initListeners(){
		$(".fa").on("click",function(){
			if($(this).hasClass("fa-chevron-up")){
				var content = $(this).parent().parent().parent().next();
				content.hide();
				$(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
			}else if($(this).hasClass("fa-chevron-down")){
				var content = $(this).parent().parent().parent().next();
				content.show();
				$(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
			}
		});
		$(".fa-refresh").on("click",function(){
			loadCardContent(CARD_INFO_OBJ[$(this).parent().parent().parent().parent().parent().attr("id")]);
		});
		setTimeout(function(){
			$(".J_menuItem").on("click",window.parent.onModuleClick);
		},1000);
		
	}
	//显示卡片错误
	function showContentError(id,html){
		html = '<div style="color:red;">' + html + '</div>';
		$("#" + id + '-content').html(html);
	}
	//显示卡片内容
	function showContentHtml(id,html){
		$("#" + id + '-content').html(html);
	}
	//加载卡片内容
	function loadCardContent(info){
		if(!info.name){
			showContentError(info.id,"没有配置URL参数");
			return;
		}
		
		$("#" + info.id + "-content").html(
			'<div class="sk-spinner sk-spinner-wave">'
	            +'<div class="sk-rect1"></div>'
	            +'<div class="sk-rect2"></div>'
	            +'<div class="sk-rect3"></div>'
	            +'<div class="sk-rect4"></div>'
	            +'<div class="sk-rect5"></div>'
        	+'</div>'
        );
		
		JcallShell.Server.ajax({
			url:JcallShell.System.Path.UI + '/sysbase/main/module/card/' + info.name + '/index.html?d=' + new Date().getTime(),
			dataType: "text",
			success:function(data, textStatus){
				var html = data.replace(/{Id}/g,info.id);
				showContentHtml(info.id,html);
				loadCardContentJs(info);
			}
		},function(data){
			if(!data.success){
				showContentError(info.id,"HTML加载发生错误," + data.msg);
			}
		});
	}
	//加载每个卡片JS
	function loadCardContentJs(info){
		JcallShell.Server.ajax({
			url:JcallShell.System.Path.UI + '/sysbase/main/module/card/' + info.name + '/index.js?d=' + new Date().getTime(),
			dataType: "text",
			success:function(data, textStatus){
				CARDS[info.id] = JcallShell.JSON.decode(data);
				if(CARDS[info.id].init){
					CARDS[info.id].init(info,function(){});
				}else{
					showContentError(info.id,"没有初始化方法init");
				}
			}
		},function(data){
			if(!data.success){
				showContentError(info.id,"JS加载发生错误," + data.msg);
			}
		});
	}
	//初始化卡片
	initCrads();
});