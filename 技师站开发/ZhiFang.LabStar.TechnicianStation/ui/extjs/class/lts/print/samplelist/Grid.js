/**
 * 打印样本清单
 * @author liangyl
 * @version 2019-12-06
 */
Ext.define('Shell.class.lts.print.samplelist.Grid', {
	extend: 'Shell.class.lts.print.basic.Grid',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '打印样本清单',
	
    /**获取样本单数据服务路径*/
	selectUrl:'',
	/**默认加载*/
	defaultLoad: false,
	  /**小组*/
	SectionID: null,
		/**带功能按钮栏*/
	hasButtontoolbar:true,
	
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift({
			text:'选择检验单',tooltip:'选择检验单',iconCls:'button-add',
	    	handler:function(but,e){
	    		JShell.Win.open('Shell.class.lts.print.transfer.App',{
					width:'95%',
					height:'95%',
					SectionID:me.SectionID,
					listeners:{
						checked : function(recs,panel){
							me.store.removeAll();
							for(var i=0;i<recs.length;i++){
								me.store.add(recs[i]);
							}
							//列表按检验日期+样本号重新排序
                            me.storeSort();
							panel.close();
						}
					}
				}).show();
	    	}
		},{
			text:'打印',tooltip:'打印',iconCls:'button-print',
				handler: function (but, e) {
					var data = [[]];
					Ext.Array.each(me.store.data.items, function (str2, index2, array2) {//遍历数组
						data[0].push(str2["data"]);
					});
					if (data.length == 0) {
						JShell.Msg.alert("请选择数据进行打印!");
						return;
					}
					data = JSON.stringify(data).replace(RegExp("LisTestForm_", "g"), "").replace(RegExp("LisPatient_", "g"), "");
				JShell.Win.open('Ext.panel.Panel', {
					title: '打印',
					width: '400px',
					height: '340px',
					listeners: {
						show: function (t) {
							var iframe = "<iframe id='printIframe' name='printIframe' width='100%' height='100%' src=" + JShell.System.Path.ROOT + "/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=1&ModelType=1></iframe>";
							t.update(iframe);
							setTimeout(function () {
								window.frames["printIframe"].frameElement.contentWindow.PrintDataStr = data;//传递JSON数据参数
							}, 200);
						}
					}
				}).show();
	    	}
		},{
			text:'设计模板',tooltip:'设计模板',margin:'0 0 0 10',
				handler: function (but, e) {
					JShell.Win.open('Ext.panel.Panel', {
						title:'设计模板',
						width: '95%',
						height: '80%',
						listeners: {
							show: function (t) {
								var iframe = "<iframe width='100%' height='100%' src=" + JShell.System.Path.ROOT + "/ui/layui/views/system/comm/JsonPrintTemplateManage/index.html?type=2&BusinessType=1&ModelType=1" + "></iframe>";
								t.update(iframe);
							}
						}
					}).show();
	    	}
		},{
	        xtype: 'radiogroup', fieldLabel: '', columns: 1, vertical: true,
			height:22,labelAlign: 'right',margin:'0 0 0 20',
	        items: [
	            { boxLabel: '样本清单',labelWidth: 90, name: 'samplelist', inputValue: '2'}
	        ]
	    },{
	        xtype: 'radiogroup', fieldLabel: '', columns: 1, vertical: true,
			height:22,labelAlign: 'right',margin:'0 0 0 20',
	        items: [
	            { boxLabel: '样本清单+项目明细',labelWidth: 90, name: 'samplelist', inputValue: '3',checked: true}
	        ]
	    },{
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '预览',
            name: 'showpdf',itemId:'showpdf',margin:'0 0 0 100',
            checked:true,//labelSeparator:'',
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            	}
            }
        });
		return buttonToolbarItems;
	}
});