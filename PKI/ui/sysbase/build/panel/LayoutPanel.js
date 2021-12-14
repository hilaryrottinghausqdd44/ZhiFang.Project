/**
 * 布局选择类
 * @author Jcall
 * @version 2014-08-25
 */
Ext.define('Shell.sysbase.build.panel.LayoutPanel',{
	extend:'Shell.ux.form.Panel',
	
	title:'应用布局类型选择',
	width:640,
	height:400,
	/**默认选中的布局类型*/
	layoutType:1,
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
//			items = [{
//			xtype:'uximage',margin:5,
//			src:Shell.util.Path.rootPath + '/ui/css/images/apptype/app-center-east.jpg',
//			info:{layout:'border'},
//			listeners:{click:function(img){me.openMaxImg(img.src);}}
//		},{
//			xtype:'uximage',margin:5,
//			src:Shell.util.Path.rootPath + '/ui/css/images/apptype/app-center-east2.jpg',
//			listeners:{click:function(img){me.openMaxImg(img.src);}}
//		},{
//			xtype:'uximage',margin:5,
//			src:Shell.util.Path.rootPath + '/ui/css/images/apptype/app-center-east3.jpg',
//			listeners:{click:function(img){me.openMaxImg(img.src);}}
//		},{
//			xtype:'uximage',margin:5,
//			src:Shell.util.Path.rootPath + '/ui/css/images/apptype/app-center-east4.jpg',
//			listeners:{click:function(img){me.openMaxImg(img.src);}}
//		}];
		
		items = [{
			xtype:'radiogroup',defaults:{name:'type',labelAlign:'top',labelSeparator:''},itemId:'type',columns:3,
			items:[{
				margin:5,boxLabel:'CENTER-EAST',inputValue:1,
				fieldLabel:"<img src='" + Shell.util.Path.rootPath + "/ui/css/images/apptype/app-center-east.jpg'/>"
			},{
				margin:5,boxLabel:'CENTER-WEST',inputValue:2,
				fieldLabel:"<img src='" + Shell.util.Path.rootPath + "/ui/css/images/apptype/app-center-west.jpg'/>"
			}]
		}];
		
		items[0].items[me.layoutType - 1].checked = true;
			
		return items;
	},
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			tool = [{dock:'bottom',buttons:['->','accept']}];
			
		if(me.floating){tool[0].buttons.push('cancel');};
			
		var toolbars = me.toolbars || tool,
			length = toolbars.length,
			dockedItems = [];
		
		for(var i=0;i<length;i++){
			dockedItems.push({
				autoScroll:true,
				dock:toolbars[i].dock || 'top',
				xtype:'uxbuttonstoolbar',
				buttons:toolbars[i].buttons,
				listeners:{
					click:function(but,type){
						me.onButtonClick(but,type);
					}
				}
			});
		}
			
		return dockedItems;
	},
	/**重写确认按钮事件*/
	onAcceptClick:function(but){
		var me = this,
			type = me.getComponent('type'),
			value = type.getValue().type,
			info = {};
			
		switch(value){
			case 1: info = {value:1,name:'CENTER-EAST',layout:{layout:'border'},info:'east'};break;
			case 2: info = {value:2,name:'CENTER-WEST',layout:{layout:'border'},info:'west'};break;
		}
		
		me.fireEvent('accept',me,but,info);
	},
	/**重写取消按钮事件*/
	onCancelClick:function(but){
		this.fireEvent('cancel',this,but);
	},
	/**打开大的图片##*/
	openMaxImg:function(src){
		if(!src) return;
		var arr = src.split('.') || [],
			length = arr.length;
		
		if(length < 2) return;
		
		arr[length-2] = arr[length-2] + '-M';
		
		Shell.util.Win.open('Shell.ux.panel.Panel',{
			width:810,height:500,bodyPadding:5,title:'布局类型图',
			items:[{xtype:'uximage',src:arr.join('.')}]
		});
	}
});