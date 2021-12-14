/**
 * 已分配与待分配
 * @author liangyl
 * @version 2017-5-10
 */
Ext.define('Shell.class.wfm.client.user.Panel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'已分配与待分配',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
    	me.SplitGrid.on({
			beforesave:function(){
				me.showMask(me.saveText);//显示遮罩
			},
			save:function(){
				me.hideMask();//隐藏遮罩
				if(me.SplitGrid.errorList.length > 0){
					JShell.Msg.error(me.SplitGrid.errorList.join('</br>'));
				}else{
					JShell.Msg.alert('保存成功！',null,1000);
					me.Grid.onSearch();
				}
			}
		});
		me.Grid.on({
			beforesave:function(){
				me.showMask(me.saveText);//显示遮罩
			},
			save:function(){
				me.hideMask();//隐藏遮罩
				if(me.Grid.errorList.length > 0){
					JShell.Msg.error(me.Grid.errorList.join('</br>'));
				}else{
					JShell.Msg.alert('保存成功！',null,1000);
					me.Grid.onSearch();
					me.SplitGrid.onSearch();
				}
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		//已分配客户列表
		me.Grid = Ext.create('Shell.class.wfm.client.user.AssignedGrid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
			
		});
		//待分配客户列表
		me.SplitGrid = Ext.create('Shell.class.wfm.client.user.SplitGrid', {
			region: 'south',
			header: false,
			height:250,
			split: true,
			itemId: 'SplitGrid'
		});
		
		return [me.Grid,me.SplitGrid];
	}
});
	