//高级表单查询--全部与查询条件配置:左下半部的表单列表编辑视图

Ext.define("ZhiFang.view.FormItemParamsView",{
	extend:'Ext.grid.Panel',
	alias: 'widget.formItemParamsView',
	id:'FormItemParamsView',
	title:'数据项属性',
	columnLines : true,//在行上增加分割线
	columns:[//列模式的集合
		{text:'隐藏ID',dataIndex:'Id',width:20,hideable:false,hidden:true},
		{xtype: 'rownumberer',text: '序号',width: 35,align: 'center'},
		{text: '交互字段',dataIndex:'EName',width:100,align: 'center',disabled: true},
		{text: '显示名称',dataIndex:'CName',width:100,align: 'center',
			editor: {
				allowBlank: false
			} 
		},
		{text: '类型',dataIndex:'Type',width:60,align: 'center',
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
                //id: 'LastFormItemParamsViewComboBox',
                model : 'local',
                editable : false, 
				displayField: 'text',valueField: 'value',
                store: new Ext.data.SimpleStore({ 
				    fields : ['value','text'], 
				    data : [
				    	['ComboBox','下拉框'],['TextField','文本框'],['TextAreaField','文本域'],['NumberField','数字框'],['DateField','日期框'],
				    	['TimeField','时间框'],['Checkbox','复选框'],['Radio','单选框'],['Label','纯文本'],['Button','按钮']
				    ]
				}),
                lazyRender: true,
                listClass: 'x-combo-list-small'
            })
		},

         
        {text: '下拉列表数据服务',dataIndex:'DataUrl',width:210,align: 'center',
//            renderer:function(value, p, record){
//                var data = Ext.getCmp('LastDataUrlComboBox').store.data;
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
                id: 'LastDataUrlComboBox',
//              model : 'local',
                model : 'ZhiFang.model.FormItemComboBoxModel',
                editable : false, 
                displayField: 'text',
                valueField: 'value',
                
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
                lazyRender: true,
                listClass: 'x-combo-list-small'
            })
        },
        
//        {text: '下拉列表数据源',dataIndex:'DataUrl',width:140,align: 'center',hideable:false,hidden:false,
//        editor: {
//            }
//        },  
		{text: '位置X',dataIndex:'X',width:60,align: 'center',
			xtype:'numbercolumn',
			format: '0',
			editor: {
                xtype: 'numberfield',
                allowBlank: false
            }
		},
		{text: '位置Y',dataIndex:'Y',width:60,align: 'center',
			xtype:'numbercolumn',
			format: '0',
			editor: {
                xtype: 'numberfield',
                allowBlank: false
            }
		},
		{text: '组件宽度',dataIndex:'Width',width:60,align: 'center',
			xtype:'numbercolumn',
			format: '0',
			editor: {
                xtype: 'numberfield',
                allowBlank: false,
                minValue: 10,
                maxValue: 999
            }
		},
		{text: '显示名称宽度',dataIndex:'LabelWidth',width:80,align: 'center',
			xtype:'numbercolumn',
			format: '0',
			editor: {
                xtype: 'numberfield',
                allowBlank: false,
                minValue: 20,
                maxValue: 50
            }
		},
		{text: '组件高度',dataIndex:'Height',width:60,align: 'center',
			xtype:'numbercolumn',
			format: '0',
			editor: {
                xtype: 'numberfield',
                allowBlank: false,
                minValue: 10,
                maxValue: 999
            }
		},
		{text: '隐藏',dataIndex:'IsHidden',width:60,align: 'center',
			xtype: 'checkcolumn',
            editor: {
                xtype: 'checkbox',
                cls: 'x-grid-checkheader-editor'
            }
		},
		{text: '路径',dataIndex:'Url',width:120,align: 'center',hidden:true},
		{text: '事件',dataIndex:'Function',width:200,align: 'center',hidden:true}
	],
	store:'FormItemParamsStore',
	viewConfig: {
        emptyText: '没有数据！',
        loadingText: '获取数据中，请等待...',
		loadMask:false
	},
	plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:2}),
	initComponent:function(){
		this.callParent(arguments);
	}
})