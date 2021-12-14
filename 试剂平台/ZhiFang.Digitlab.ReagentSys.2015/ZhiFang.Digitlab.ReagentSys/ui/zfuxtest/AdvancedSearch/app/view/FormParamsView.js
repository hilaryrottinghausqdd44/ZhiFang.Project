//高级表单查询--全部与查询条件配置:高级查询的右边的表单配置选择视图

Ext.define("ZhiFang.view.FormParamsView",{
	extend:'Ext.form.Panel',
	id:'FormParamsView',
	alias: 'widget.formParamsView',
	
	layout: 'accordion',
    title:'高级查询表单配置',
    
	items:[
        {
		xtype: 'form',
		id: 'FormParamsView_HeadParams',
		autoScroll: true,
        bodyPadding: 10,
        collapsed: true,
        title: '标题属性',
        items: [
              {
            xtype: 'textfield',fieldLabel: '标题ID',name: 'Form_Id',
            labelWidth: 55,anchor: '100%',value:"LabStar_AdvancedSearch"+Ext.id()
        },
                {
            xtype: 'textfield',fieldLabel: '标题名称',name: 'Title',
            labelWidth: 55,anchor: '100%',value: '表单'
        },{
            xtype: 'combobox',fieldLabel: '字体大小',name: 'Size',
            labelWidth: 55,anchor: '100%',value: 0,
            id: 'FormParamsView_Size',
            mode : 'local',editable : false, 
			displayField: 'text',valueField: 'value',
			store: new Ext.data.SimpleStore({ 
			    fields : ['value','text'], 
			    data : [[0,'默认'],[8,'小'],[11,'中'],[14,'大']]
			})
        },{
            xtype: 'combobox',fieldLabel: '字体颜色',name: 'Color',
            labelWidth: 55,anchor: '100%',value: '',
            id: 'FormParamsView_Color',
            mode : 'local',editable : false, 
			displayField: 'text',valueField: 'value',
			store: new Ext.data.SimpleStore({ 
			    fields : ['value','text'], 
			    data : [
			    	['','默认'],
			    	['black','黑色'],['white','白色'],
			    	['red','红色'],['orange','橙色'],
			    	['yellow','黄色'],['green','绿色'],
			    	['blue','蓝色'],['purple','紫色']
			    ]
			})
        },{
            xtype: 'combobox',fieldLabel: '字体类型',name: 'Style',
            labelWidth: 55,anchor: '100%',value: '',
            id: 'FormParamsView_Style',
            mode : 'local',editable : false, 
			displayField: 'text',valueField: 'value',
			store: new Ext.data.SimpleStore({ 
			    fields : ['value','text'], 
			    data : [['','默认'],['宋体','宋体'],['楷书','楷书'],['隶书','隶书']]
			})
        }]
    },{
        xtype: 'form',
        id: 'FormParamsView_ColumnParams',
        autoScroll: true,
        bodyPadding: 10,
        collapsed: false,
        title: '数据项模板属性',
        items: [{
        	xtype: 'button',id:'ColumnOk',text: '确定',iconCls: 'ok'
        },
        { 
        //region: 'east',
        xtype: 'textfield',
        id:"FormParamsView_FormModel",
        labelWidth :80,
        width : 380,
        emptyText : "请填写数据服务地址:如server/GetFormItemInfo.json",
        value:"server/GetFormItemInfo.json",
        fieldLabel: '数据服务地址 '
            },
            { 
        //region: 'east',
        xtype: 'textfield',
        id:"ComboBoxObjectUrl",
        labelWidth :80,
        width : 380,
        emptyText : "请填写下拉列表数据服务地址:如server/GetComboBoxObjectList.json",
        value:"server/GetComboBoxObjectList.json",
        fieldLabel: '下拉列表服务 '
            },
           {
            xtype: 'button',id:'btnSelect',text: '查询',iconCls: 'ok'
        },
        //换成下拉列表
             {
                    xtype:'combobox',fieldLabel:'数据对象',name:'ObjectList',
                    id: 'ObjectListCombobox',labelWidth:65,anchor:'100%',
                    //editable:false,
                    typeAhead:true,
                    forceSelection:true,//要求输入的值必须在列表中存在
                    mode:'local',//申明本属性即可实现过滤 
                    emptyText:'请选择数据对象',
                    displayField:'CName',
                    valueField:'Object'
                },  
            {
        	xtype: 'gridpanel',
        	id: 'FormParamsView_FormModelGrid',
        	selType:'checkboxmodel',//复选框
			multiSelect:true,//允许多选
            simpleSelect:true,    //简单选择功能开启  
			sortableColumns:false,
			hideable:false,
			border:false,
        	columns:[//列模式的集合
			{text: '显示名称',dataIndex:'CName',width:60,align: 'center'},
			{text: '交互字段',dataIndex:'EName',width:60,align: 'center',hideable:false,hidden:true},
			{text: '类型',dataIndex: 'Type',width:60,align: 'center',hideable:false,hidden:true},       
            {text: '类型',dataIndex:'Type',width:70,align: 'center',
            renderer:function(value, p, record){
                var data = Ext.getCmp('FormItemParamsViewComboBox').store.data;
                var arr = data.items;
                for(var i in arr){
                    if(arr[i]['raw'][0] === value){
                        return Ext.String.format(arr[i]['raw'][1]);
                    }
                }
                return Ext.String.format("");
            },
            editor: new Ext.form.field.ComboBox({
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                id: 'FormItemParamsViewComboBox',
                model : 'local',
                editable : false, 
                displayField: 'text',valueField: 'value',
                store: new Ext.data.SimpleStore({ 
                   // fields : ['value','text'],
                    model : 'ZhiFang.model.FormItemComboBoxModel',
                    data : [
                        ['ComboBox','下拉框'],['TextField','文本框'],['TextAreaField','文本域'],['NumberField','数字框'],['DateField','日期框'],
                        ['TimeField','时间框'],['Checkbox','复选框'],['Radio','单选框']
                    ]
                }),
                lazyRender: true,
                listClass: 'x-combo-list-small'
            })
        },
        
        {text: '下拉列表数据服务',dataIndex:'DataUrl',width:210,align: 'center',
//            renderer:function(value, p, record){
//                var data = Ext.getCmp('DataUrlComboBox').store.data;
//                var arr = data.items;
//                for(var i in arr){
//                    if(arr[i]['raw'][0] === value){
//                        return Ext.String.format(arr[i]['raw'][1]);
//                    }
//                }
//                return Ext.String.format("");
//            },
            
            editor: new Ext.form.field.ComboBox({
                typeAhead: true,
                triggerAction: 'all',
                selectOnTab: true,
                id: 'DataUrlComboBox',
                //model : 'local',
                //model : 'FormItemComboBoxModel',
                //editable : false, 
                displayField: 'text',
                valueField: 'value',
                //store:'FormItemComboBoxStore',
                
                store: new Ext.data.SimpleStore({ 
                    //fields : ['value','text'],       
                    model : 'ZhiFang.model.FormItemComboBoxModel',
                    proxy:{  
                    type: 'ajax',
                    url: '../data/GetListDictionaryData.json',
                    reader: {
                     type: 'json',
                     root: 'list'
                     }
                }
                }),
                
                //lazyRender: true,
                listClass: 'x-combo-list-small'
            })
        }        
			],
            plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:2}),
			viewConfig: {
		        emptyText: '没有数据！',
		        loadingText: '获取数据中，请等待...',
				loadMask:false
			}
        }]
        
    },{
    	xtype: 'form',
        id: 'FormParamsView_Function',
    	autoScroll: true,
        bodyPadding: 10,
        collapsed: true,
        title: '内部动作',
        items:[{
        	xtype: 'checkbox',boxLabel: '公开',name: 'Refresh',
            fieldLabel: '刷新',checked: true,
            labelWidth: 65,anchor: '100%' 
        }]
    }],
	initComponent:function(){
		this.callParent(arguments);
	}
})