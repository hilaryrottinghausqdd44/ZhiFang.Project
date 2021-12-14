/**
 * 任务操作记录
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.operate.Panel',{
    extend: 'Ext.panel.Panel',
    title:'任务操作记录',
    
    autoScroll:true,
    
    /**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskOperLogByHQL?isPlanish=true',
    
    /**任务ID*/
    TaskId:null,
    
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onLoadData();
	},
	initComponent:function(){
		var me = this;
		me.dockedItems = {
			xtype:'toolbar',
			dock:'top',
			items:[{
				xtype:'button',
				text:'刷新数据',
				iconCls:'button-refresh',
				handler:function(){
					me.onLoadData();
				}
			}]
		};
		me.callParent(arguments);
	},
	onLoadData:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);
			
		var fields = ['Id','PTaskOperTypeID','OperaterID','OperaterName','DataAddTime','OperateMemo'];
		url += '&fields=PTaskOperLog_' + fields.join(',PTaskOperLog_');
		url += '&where=ptaskoperlog.PTaskID=' + me.TaskId;
		url += '&sort=[{"property":"PTaskOperLog_DataAddTime","direction":"ASC"}]';
		
		me.showMask('数据加载中...');
		JcallShell.Server.get(url,function(data){
			me.hideMask();
			if(data.success){
				if(data.value){
					me.changeHtml(data.value.list);
				}else{
					var html = '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">没有操作记录</div>';
					me.update(html);
				}
			}else{
				var html = '<div style="color:red;text-align:center;margin:20px 10px;font-weight:bold;">' + data.msg + '</div>';
				me.update(html);
			}
		});
	},
	/**更改页面内容*/
	changeHtml:function(list){
		var me = this,
			arr = list || [],
			len = arr.length,
			html = [];
		
		for(var i=0;i<len;i++){
			var data = arr[i];
			html.push('<div style="margin:5px;">');
			html.push(JShell.Date.toString(data.PTaskOperLog_DataAddTime) + ' ');
			html.push(data.PTaskOperLog_OperaterName + ' ');
			var OperTypeInfo = JShell.WFM.GUID.getInfoByGUID('TaskStatus',data.PTaskOperLog_PTaskOperTypeID);
			var OperTypeName = OperTypeInfo ? OperTypeInfo.text : '';
			
			var style = [];
			if(OperTypeInfo.bgcolor){style.push('color:' + OperTypeInfo.bgcolor);}
			
			html.push('<b style="' + style.join(';') + '">' + OperTypeName + '</b> ');
			
			if(data.PTaskOperLog_OperateMemo){
				html.push('处理意见：<b>' + data.PTaskOperLog_OperateMemo + '</b>');
			}
			
			html.push('</div>');
		}
		
		me.update(html.join(''));
	},
	/**显示遮罩*/
	showMask: function(text) {
		this.body.mask(text);
	},
	/**隐藏遮罩*/
	hideMask: function() {
		this.body.unmask();
	}
});