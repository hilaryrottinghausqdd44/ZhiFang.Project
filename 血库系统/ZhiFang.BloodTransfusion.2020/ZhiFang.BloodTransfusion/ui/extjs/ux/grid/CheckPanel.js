/**
 * 选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.grid.CheckPanel',{
    extend:'Shell.ux.grid.Panel',
    title:'选择列表',
    width:270,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'',
    /**默认加载*/
	defaultLoad:true,
	/**后台排序*/
	remoteSort:true,
	/**带分页栏*/
	hasPagingtoolbar:true,
	/**是否启用序号列*/
	hasRownumberer:true,
	/**默认选中*/
	autoSelect:false,
	/**是否单选*/
	checkOne:true,
	/**是否带清除按钮*/
	hasClearButton:true,
	/**是否带确认按钮*/
	hasAcceptButton:true,
	
	/**查询框信息*/
	searchInfo:{emptyText:'',isLike:true,fields:[]},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//单选双击触发确认事件
		if(me.checkOne){
			me.on({
				itemdblclick:function(view,record){
					me.fireEvent('accept',me,record);
				}
			});
		}
	},
	initComponent:function(){
		var me = this;
		//me.addEvents('accept');
		
		if(!me.checkOne){
			//复选框
			me.multiSelect = true;
			me.selType = 'checkboxmodel';
		}
		me.initButtonToolbarItems();
		
		me.callParent(arguments);
	},
	initButtonToolbarItems:function(){
		var me = this;
		
		if(me.checkOne){
			if(!me.searchInfo.width) me.searchInfo.width = 145;
			//自定义按钮功能栏
			me.buttonToolbarItems = me.buttonToolbarItems || [];
			me.buttonToolbarItems.push({type:'search',info:me.searchInfo});
			
			if(me.hasClearButton){
				me.buttonToolbarItems.unshift({
					text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
					handler:function(){me.fireEvent('accept',me,null);}
				});
			}
			if(me.hasAcceptButton){
				me.buttonToolbarItems.push('->','accept');
			}
		}else{
			if(!me.searchInfo.width) me.searchInfo.width = 205;
			//自定义按钮功能栏
			me.buttonToolbarItems = me.buttonToolbarItems || [];
			me.buttonToolbarItems.push({type:'search',info:me.searchInfo});
			if(me.hasAcceptButton) me.buttonToolbarItems.push('->','accept');
		}
	},
	/**确定按钮处理*/
	onAcceptClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		
		if(me.checkOne){
			if(records.length != 1){
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			me.fireEvent('accept',me,records[0]);
		}else{
			if(records.length == 0){
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}
			me.fireEvent('accept',me,records);
		}
	}
});