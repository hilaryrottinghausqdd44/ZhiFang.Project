/**
 * 下拉勾选列表
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.form.field.ComboGridBox',{
	extend:'Ext.form.field.ComboBox',
	alias:['widget.uxcombogridbox','widget.uxcombogrid'],
	
	requires:['Shell.ux.panel.Grid'],
	mixins:['Shell.ux.server.Ajax'],
	
	/**是否多选*/
	multiSelect:true,
	/**弹出框是否与组件宽度相同*/
	matchFieldWidth:true,
	/**是否忽略大小写*/
    ignoreCase:true,
    /**查询框宽度*/
    searchWidth:120,
    
    minPickerHeight:100,
    maxPickerHeight:300,
	
	/**数据服务地址*/
	url:null,
	/**显示值字段*/
	displayField:'text',
	/**真实值字段*/
	valueField:'value',
	/**时间戳字段*/
	timestampField:'',
	/**查询的字段*/
	searchFields:[],
	/**查询框空值*/
	searchEmptyText:'',
	
	/**默认选中的真实值*/
	defaultSelect:null,
	/**加载的数据数量*/
	dataSize:100,
	
	/**初始化组件属性*/
	initComponent:function(){
		var me = this;
		me.editable = false;//输入框不可修改
		me.columns = me.createColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createColumns:function(){
		return this.columns;
	},
	
	/**重写弹出框*/
	createPicker:function(){
		var me = this,
			picker,
			pickerCfg = {
				xtype:'uxgrid',
				selModel:{mode:me.multiSelect?'SIMPLE':'SINGLE'},
				floating:true,
            	hidden:true,
            	columnLines:true,
            	focusOnToFront:false,
            	resizable:!me.matchFieldWidth,
            	
				tooltip:true,
				showErrorInfo:false,
				autoSelect:false,
				defaultLoad:true,
				minHeight:me.minPickerHeight,
				maxHeight:me.maxPickerHeight,
				defaultPageSize:me.dataSize,
				searchFields:me.searchFields,
				pagingtoolbar:'basic',
				selectUrl:me.url,
				columns:me.columns || [],
				toolbars:[
					{dock:'top',buttons:[
						'refresh','-',
						{btype:'searchtext',width:me.searchWidth,emptyText:me.searchEmptyText,
							triggerCls:'x-form-clear-trigger',
							onTriggerClick:function(){this.setValue('');},
							listeners:{
								change:function(field,newValue){
									me.picker.searchValue = newValue;
									me.picker.store.filterBy(function(record){return me.filterFn(record);});
								},
					            keyup:{
					                fn:function(field,e){
					                	if(e.getKey() == Ext.EventObject.ESC){
					                		field.setValue('');
					                	}
					                }
					            }
					        }
						},'-','cancel']
					}
				],
				storeConfig:{indexOf:function(record){
					if(!this.snapshot) return -1;
					return this.snapshot.indexOf(record);
				}},
				onCancelClick:function(){
					me.setValue([]);
				}
			};
			
		if(!me.matchFieldWidth){pickerCfg.width = me.width - me.labelWidth - me.labelPad;}
			
		picker = me.picker = Ext.widget(pickerCfg);
		picker.store.on({
			load:function(store,records,successful){
				if(!successful) return;
				store.filterBy(function(record){return me.filterFn(record);});
			}
		});
		
		me.mon(picker, {
            itemclick: me.onItemClick,
            refresh: me.onListRefresh,
            scope: me
        });

        me.mon(picker.getSelectionModel(), {
            beforeselect: me.onBeforeSelect,
            beforedeselect: me.onBeforeDeselect,
            selectionchange: me.onListSelectionChange,
            scope: me
        });
		
		return picker;
	},
    /**重写选中变化处理*/
    onListSelectionChange: function(list, selectedRecords) {
        var me = this;
        me.callParent(arguments);
        Shell.util.Msg.showLog(me.getValue());
    },
    /**重写默认选中处理*/
    doAutoSelect: function() {
        var me = this,
            picker = me.picker,
            lastSelected, itemNode;
        if (picker && me.autoSelect && me.store.getCount() > 0) {
            // Highlight the last selected item and scroll it into view
            lastSelected = picker.getSelectionModel().lastSelected;
            itemNode = picker.view.getNode(lastSelected || 0);
            if (itemNode) {
                picker.view.highlightItem(itemNode);
                picker.view.el.scrollChildIntoView(itemNode, false);
            }
        }
    },
    /**过滤数据*/
    filterFn:function(record){
    	var me = this,
    		picker = me.picker,
    		searchFields = me.searchFields,
    		length = searchFields.length,
			value = picker.searchValue || '',
			v = '';
			
		if(value == '') return true;
		
		if(me.ignoreCase){value = value.toLowerCase();}
			
		for(var i=0;i<length;i++){
			v = record.get(searchFields[i]) || '';
			v = me.ignoreCase ? v.toLowerCase() : v;
			if(v.indexOf(value) != -1) return true;
		}
		
		return false;
	},
	setValue: function(value, doSelect) {
		return this.callParent(arguments);
	}
});