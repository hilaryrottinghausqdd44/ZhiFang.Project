/**
 * 仪器项目选择
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.testitem.equipitemgoodlink.EquipItemCheckGrid',{
    extend:'Shell.class.rea.client.basic.CheckPanel',
    title:'项目选择',
    width:450,
    height:350,
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipItemByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:false,
	TestItemEnum:{},
	/**默认每页数量*/
	defaultPageSize: 1000,
	initComponent:function(){
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			dataIndex: 'ReaTestEquipItem_TestItemID',text: '项目编号',
			width: 150,defaultRenderer: true
		},{
			dataIndex: 'ReaTestEquipItem_TestItemCName',text: '项目名称',
			minWidth: 150,flex:1,defaultRenderer: true
		},{
			dataIndex: 'ReaTestEquipItem_Id',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
		for(var i=0;i<data.list.length;i++){
			var TestItemCName = me.TestItemEnum[data.list[i].ReaTestEquipItem_TestItemID];
			var obj1={
				ReaTestEquipItem_TestItemCName:TestItemCName
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);
		}
		result.list = arr;
		return result;
	},
	initButtonToolbarItems:function(){
		var me = this;
		if(me.checkOne){
			if(!me.searchInfo.width) me.searchInfo.width = 145;
			//自定义按钮功能栏
			me.buttonToolbarItems = me.buttonToolbarItems || [];
			me.buttonToolbarItems.push({
				xtype:'trigger',emptyText:'项目编号/项目名称',
				triggerCls:'x-form-search-trigger',enableKeyEvents:true,
				onTriggerClick:function(){
				},
				listeners:{
		            keyup:{
		                fn:function(field,e){
		                	if(e.getKey() == Ext.EventObject.ESC){
		                		field.setValue('');
		                		me.filterFn(Ext.util.Format.lowercase(field.getValue()));
		                	}else if(e.getKey() == Ext.EventObject.ENTER){
								me.filterFn(Ext.util.Format.lowercase(field.getValue()));
		                	}
		                }
		            }
		        }
			});
			
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
			me.buttonToolbarItems.push({
				xtype:'trigger',emptyText:'项目编号/项目名称',
				triggerCls:'x-form-search-trigger',enableKeyEvents:true,
				onTriggerClick:function(){
				},
				listeners:{
		            keyup:{
		                fn:function(field,e){
		                	if(e.getKey() == Ext.EventObject.ESC){
		                		field.setValue('');
                                me.filterFn(Ext.util.Format.lowercase(field.getValue()));
		                	}else if(e.getKey() == Ext.EventObject.ENTER){
                                me.filterFn(Ext.util.Format.lowercase(field.getValue()));
		                	}
		                }
		            }
		        }
			});
//			me.buttonToolbarItems.push({type:'search',info:me.searchInfo});
			if(me.hasAcceptButton) me.buttonToolbarItems.push('->','accept');
		}
	},
	filterFn: function(value) {
        var me = this,
        valtemp = value;
        var store = me.getStore();
        if (!valtemp) {
            store.clearFilter();
            return
        }
        valtemp = String(value).trim().split(" ");
        store.filterBy(function(record, id) {
            var data = record.data;
            var TestItemCName = record.data.ReaTestEquipItem_TestItemCName;
            var TestItemID = record.data.ReaTestEquipItem_TestItemID;
            var dataarr = {
                ReaTestEquipItem_TestItemCName: TestItemCName,
                ReaTestEquipItem_TestItemID: TestItemID
            };
            for (var p in dataarr) {
                var porp = Ext.util.Format.lowercase(String(dataarr[p]));
                for (var i = 0; i < valtemp.length; i++) {
                    var macther = valtemp[i];
                    var macther2 = Ext.escapeRe(macther);
                    mathcer = new RegExp(macther2);
                    if (mathcer.test(porp)) {
                        return true
                    }
                }
            }
            return false
        })
    }
});