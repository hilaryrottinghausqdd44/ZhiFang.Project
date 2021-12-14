/**
 * 样本信息合并
 * @author liangyl
 * @version 2019-11-19
 */
Ext.define('Shell.class.lts.merge.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'样本信息合并',
    width:240,
    bodyPadding:5,
	SectionID:null,
	//布局方式
	layout:'anchor',
	//每个组件的默认属性
	defaults:{
		anchor:'100%',
		labelWidth:60,
		labelAlign:'right'
	},
	formtype:'edit',
    //检验日期
    defaultDate:null,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		//查询展开
		setTimeout(function () {
			me.getComponent('SSample').getComponent('LBSection_CName').onTriggerClick();
			me.getComponent('DSample').getComponent('DLBSection_CName').onTriggerClick();
		}, 500);
	},
	initComponent:function(){
		var me = this;
		me.initDate();
		me.addEvents('accept');
		me.callParent(arguments);
	},
	//@overwrite 创建内部组件
	createItems:function(){
		var me = this,
			items = [];
			
		items.push({
            xtype: 'fieldset',title: '样本信息',
            defaultType: 'textfield',layout: 'anchor',defaults:me.defaults,
            items :[{
		        xtype: 'checkboxgroup', fieldLabel: '',columns: 1,vertical: true,
		        items: [
		            { boxLabel: '样本号', name: 'isSampleNoMerge'},
		            { boxLabel: '条码号+采样信息', name: 'isSerialNoMerge',checked: true},
		        ]
		    }]
       },{
	        xtype: 'radiogroup', fieldLabel: '', columns: 1, vertical: true,
			height:22,labelAlign: 'right',
	        items: [
	            { boxLabel: '仅合并样本信息',labelWidth: 90, name: 'mergeType', inputValue: '1'}
	        ]
	   },{
	        xtype: 'radiogroup', fieldLabel: '', columns: 1, vertical: true,
			height:22,labelAlign: 'right',
	        items: [
	            { boxLabel: '仅合并项目信息',labelWidth: 90, name: 'mergeType', inputValue: '2'}
	        ]
	    },{
	        xtype: 'radiogroup', fieldLabel: '', columns: 1, vertical: true,
			height:22,labelAlign: 'right',
	        items: [
	            { boxLabel: '两者均合并',labelWidth: 90, name: 'mergeType', inputValue: '3',checked: true}
	        ]
	    },{
            xtype:'fieldset',title: '源样本选择条件',collapsible: true,itemId: 'SSample',
            defaultType: 'textfield',layout: 'anchor',defaults: me.defaults,
            items :[{
				fieldLabel: '检验小组Id',name: 'LBSection_ID',itemId: 'LBSection_ID',hidden: true
			}, {
                fieldLabel: '检验小组',name: 'LBSection_CName',itemId:'LBSection_CName',
                xtype: 'uxCheckTrigger',className: 'Shell.class.lts.section.CheckGrid',
					classConfig: { checkOne: true, autoSelect: false, SectionID: me.SectionID }
            },{
                xtype: 'datefield',format: 'Y-m-d',fieldLabel: '检验日期',
				name: 'GTestDate',itemId: 'GTestDate',value:me.defaultDate
            },{
                fieldLabel:'样本号',name:'GSampleNo',itemId:'GSampleNo'
            }]
        },{
            xtype:'fieldset',title: '目标样本选择条件', collapsible: true,itemId: 'DSample',
            defaultType: 'textfield',layout: 'anchor',defaults: me.defaults,
            items :[{
				fieldLabel: '检验小组Id',name: 'DLBSection_ID',itemId: 'DLBSection_ID',hidden: true
			}, {
				fieldLabel: '检验小组',name: 'DLBSection_CName',itemId: 'DLBSection_CName',
				xtype: 'uxCheckTrigger',className: 'Shell.class.lts.section.CheckGrid',
				classConfig:{checkOne: true, autoSelect: false, SectionID: me.SectionID}
			},{
                xtype: 'datefield',format: 'Y-m-d',fieldLabel: '检验日期',
				name: 'DGTestDate',itemId: 'DGTestDate',value:me.defaultDate
            },{
                fieldLabel:'样本号',name:'DGSampleNo',itemId: 'DGSampleNo'
            }]
        },{
	        xtype: 'panel', fieldLabel: '', border:false,itemId:'btnPanel',
			layout: {type: 'auto',pack: 'start', align: 'middle' },defaults: {},
			items: [{
				text: '确定', tooltip: '确定', iconCls: 'button-accept', xtype: 'button', width:60,
			    handler:function(but,e){
			    	var values = me.getForm().getValues();
			    	if(!values.LBSection_ID){
						JShell.Msg.alert('请选择源样本检验小组!');
						return;
					}
			        if(!values.DLBSection_ID){
						JShell.Msg.alert('请选择目标样本检验小组!');
						return;
					}
			        if(!values.GTestDate){
						JShell.Msg.alert('请选择源样本检验日期!');
						return;
					}
			        if(!values.DGTestDate){
						JShell.Msg.alert('请选择目标样本检验日期!');
						return;
					}
			        
			        if(!values.GSampleNo){
						JShell.Msg.alert('源样本号不能为空!');
						return;
					}
			        if(!values.DGSampleNo){
						JShell.Msg.alert('目标样本号不能为空!');
						return;
					}
			        if(values.GSampleNo == values.DGSampleNo && 
			        	values.LBSection_ID == values.DLBSection_ID && 
			        	values.GTestDate == values.DGTestDate){
						JShell.Msg.alert('源样本与目标样本合同条件不能相同!');
						return;
					}
                    me.fireEvent('accept',values);
			    } 
			},{
				xtype:'checkboxfield', boxLabel: '源项目合并后删除',
				name: 'isDelFormTestItem',itemId:'isDelFormTestItem',
				checked:false,//labelSeparator:'',
				listeners : {
            		change : function(com,newValue,oldValue,eOpts ){
            		}
				}
			},{
				xtype:'checkboxfield', boxLabel: '源项目合并后,如果没有项目,删除源样本单',
				name: 'isDelFormTestForm',itemId:'isDelFormTestForm',
				checked:false,//labelSeparator:'',
				listeners : {
            		change : function(com,newValue,oldValue,eOpts ){
            		}
				}
			},{
				text: '执行合并', tooltip: '执行合并', iconCls: 'button-accept', xtype: 'button',width:80,
				handler:function(but,e){
					var isDelFormTestItem = me.getComponent("btnPanel").getComponent("isDelFormTestItem").getValue(),
						isDelFormTestForm = me.getComponent("btnPanel").getComponent("isDelFormTestForm").getValue();
					me.fireEvent('save', isDelFormTestItem, isDelFormTestForm,me);
				}
			}]
	    });
		return items;
	},
	/**初始化检验日期*/
	initDate: function() {
		var me = this;
		var Sysdate = JShell.System.Date.getDate();
//		var Sysdate=new Date();
		
		me.defaultDate =  JShell.Date.toString(Sysdate,true)
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
			//源检验小组
		var CName = me.getComponent('SSample').getComponent('LBSection_CName'),
			ID = me.getComponent('SSample').getComponent('LBSection_ID');
		if(CName) {
			CName.on({
				check: function(p, record) {
					CName.setValue(record ? record.get('LBSection_CName') : '');
					ID.setValue(record ? record.get('LBSection_Id') : '');
					p.close();
				}
			});
		}
		//检验小组(目标)
		var DCName = me.getComponent('DSample').getComponent('DLBSection_CName'),
			DID = me.getComponent('DSample').getComponent('DLBSection_ID');
		if(DCName) {
			DCName.on({
				check: function(p, record) {
					DCName.setValue(record ? record.get('LBSection_CName') : '');
					DID.setValue(record ? record.get('LBSection_Id') : '');
					p.close();
				}
			});
		}
	}
});