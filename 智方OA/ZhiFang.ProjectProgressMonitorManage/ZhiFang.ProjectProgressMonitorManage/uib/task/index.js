$(function(){
	//初始化卡片
	function initGridView(list){
		var arr = list || [],
			len = arr.length,
			html = [];
			
		html.push('<table class="table table-hover"><tbody>');
		for(var i=0;i<len;i++){
			html.push(createCardHtml(arr[i]));
		}
		html.push('</tbody></table>');
		$("#GridView").html(html.join(""));
	}
	//创建卡片内容
	function createCardHtml(info){
		var html = 
            '<tr>'
                +'<td class="project-status">'
                    +'<span class="label label-primary">{Status}'
                +'</td>'
                +'<td class="project-title">'
                    +'<a>{Name}</a>'
                    +'<br/>'
                    +'<small>创建于 {DataAddTime}</small>'
                +'</td>'
                +'<td class="project-completion">'
                        +'<small>当前进度： {}%</small>'
                        +'<div class="progress progress-mini">'
                            +'<div style="width: {}%;" class="progress-bar"></div>'
                        +'</div>'
                +'</td>'
                +'<td class="project-people">'
                +'</td>'
                +'<td class="project-actions">'
                    +'<a data="{Id}" class="btn btn-white btn-sm"><i class="fa fa-folder"></i> 查看 </a>'
                    +'<a data="{Id}" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i> 编辑 </a>'
                +'</td>'
            +'</tr>';
		
		html = html.replace(/{Id}/g,info.Id);
		html = html.replace(/{ColCount}/g,info.colCount);
		html = html.replace(/{Title}/g,info.title);
		
		return html;
	}
	//初始化卡片
	//initCrads(cardInfoList);
	
	$(".ShowButton").on("click",function(){
		JcallShell.Page.Layer.open({
			title: '任务详情',
			content: JShell.System.Path.UI + "/task/info_show.html"
		});
	});
	$(".EditButton").on("click",function(){
		JcallShell.Page.Layer.open({
			title: '任务信息编辑',
			content: JShell.System.Path.UI + "/task/info_edit.html"
		});
	});
	$(".AddButton").on("click",function(){
		JcallShell.Page.Layer.open({
			title: '创建新任务',
			content: JShell.System.Path.UI + "/task/info_add.html"
		});
	});
});