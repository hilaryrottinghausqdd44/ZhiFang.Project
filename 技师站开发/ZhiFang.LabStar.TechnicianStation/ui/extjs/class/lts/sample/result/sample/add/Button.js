/**
 * 新增检验项目按钮
 * @author zhangda
 * @version 2020-04-17
 * @desc Jcall 2020-09-10修改代码部分内容
 */
Ext.define("Shell.class.lts.sample.result.sample.add.Button",{
	extend:'Ext.panel.Panel',
	title:'操作',
	width:90,
	height:200,
	bodyPadding:1,
	layout:{type:'vbox',align:'center',pack:'center'},
	defaultType:'button',
	defaults:{width:'100%',margin:5},
	
	initComponent:function (){
		var me = this;
		
		//创建挂靠功能栏
		me.dockedItems = [{
			xtype:'toolbar',
			dock:'top',
			items:[{xtype:'label',text:'操作',style:'margin:2px 5px;'}]
		}];
		
		me.items = [{
			text:'加入',iconCls:'button-right',
			handler:function(){
				me.fireEvent('addOne',me);
			}
		},{
			text:'删除',iconCls:'button-left',
			handler:function(){
				me.fireEvent('delOne',me);
			}
		},{
			text:'组合删除',iconCls:'right16_2',
			handler:function(){
				me.fireEvent('delAll',me);//删除该组合下所有
			}
		},{
			text:'重置',iconCls:'button-back',
			handler:function(){
				me.fireEvent('reset',me);
			}
		},{
			text:'保存',iconCls:'button-save',
			handler:function(){
				me.fireEvent('save',me);
			}
		}];
		
		me.callParent(arguments);
	}
});
