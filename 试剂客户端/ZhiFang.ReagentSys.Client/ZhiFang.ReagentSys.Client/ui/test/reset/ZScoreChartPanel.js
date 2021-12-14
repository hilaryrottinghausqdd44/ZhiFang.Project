Ext.ns('Ext.iqc');
Ext.Loader.setConfig({enabled: true});//允许动态加载
Ext.Loader.setPath('Ext.iqc',getRootPath()+'/ui/iqc/class');
Ext.define('Ext.iqc.chart.ZScoreChartPanel',{
	extend:'Ext.panel.Panel',
	alias:'widget.zscorechartpanel',
	
	requires:['Ext.iqc.chart.ZScoreChartBasic'],
	
	title:'图表面板',
	
	initComponent:function(){
		var me = this;
		me.layout = 'fit',
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**
	 * 初始化视图
	 * @private
	 */
	createItems:function(){
		var me = this;
		var items = [{
			xtype:'zscorechartbasic',
			itemId:'zscorechartbasic'
		}];
		return items;
	},
	/**
	 * 创建挂靠功能
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var dockedItems = [{
			xtype:'toolbar',
			items:[{
//				text:'更新数据',itemId:'load',handler:function(){
//					var data = me.getData();
//					me.getChart().loadData(data);
//				}
//			},{
//				text:'更新数据(后台)',itemId:'load2',handler:function(){
//					var params = {
//						ids:'1074,1042,5369025018050817641',
//						start:'2014-03-01',
//						end:'2014-03-31'
//					};
//					me.getChart().load(params);
//				}
//			},{
//				text:'更新数据2(后台)',itemId:'load3',handler:function(){
//					var params = {
//						ids:'1074,1042,5369025018050817641',
//						start:'2014-03-05',
//						end:'2014-03-25'
//					};
//					me.getChart().load(params);
//				}
//			},{
//				text:'更新数据3(后台)',itemId:'load4',handler:function(){
//					var params = {
//						ids:'1074,1042,5369025018050817641',
//						start:'2014-03-07',
//						end:'2014-03-20'
//					};
//					me.getChart().load(params);
//				}
//			},{
				text:'批次',itemId:'batch',handler:function(but){
					me.getChart().changeToDateChart(false);
				}
			},{
				text:'日期',itemId:'date',handler:function(but){
					me.getChart().changeToDateChart(true);
				}
			},{
				xtype:'button',itemId:'saveImg',text:'保存图片',
				handler:function(){
					me.getChart().save({type:'image/png'});
				}
			}]
		}];
		return dockedItems;
	},
	/**
	 * 获取图组件
	 * @private
	 * @return {}
	 */
	getChart:function(){
		var me = this;
		var chart = me.getComponent('zscorechartbasic');
		return chart;
	},
	/**
	 * 加载数据
	 * @public
	 * @param {} params
	 * @example
	 * var params = {
	 *     ids:'1074,1042,5369025018050817641',
	 *     start:'2014-03-07',
	 *     end:'2014-03-20'
	 * };
	 */
	load:function(params){
		var me = this;
		me.getChart().load(params);
	},
	
	/**
	 * 获取数据-测试
	 * @private
	 * @return {}
	 */
	getData:function(){
		//随机数字
	    var getRandom = function(min,max){
	    	var num = parseFloat(Math.random() * (max - min)) - 4;
	    	num = num == 0 ? num + min : num + min + 1;
	        return num;
	    };
		var data = {
			QCGraphCustomDataList:[{
				XData:'2',
				XDateData:'2012-01-01',
				YData:getRandom(0,8),
				LineName:'高值',
				QCDValue:{}
			},{
				XData:'1',
				XDateData:'2012-01-01',
				YData:getRandom(0,8),
				LineName:'低值',
				QCDValue:{}
			},{
				XData:'3',
				XDateData:'2012-01-02',
				YData:getRandom(0,8),
				LineName:'高值',
				QCDValue:{}
			},{
				XData:'3',
				XDateData:'2012-01-02',
				YData:getRandom(0,8),
				LineName:'低值',
				QCDValue:{}
			},{
				XData:'2',
				XDateData:'2012-01-03',
				YData:getRandom(0,8),
				LineName:'低值',
				QCDValue:{}
			},{
				XData:'1',
				XDateData:'2012-01-03',
				YData:getRandom(0,8),
				LineName:'高值',
				QCDValue:{}
			},{
				XData:'4',
				XDateData:'2012-01-04',
				YData:getRandom(0,8),
				LineName:'高值',
				QCDValue:{}
			},{
				XData:'4',
				XDateData:'2012-01-04',
				YData:getRandom(0,8),
				LineName:'低值',
				QCDValue:{}
			}],
			QCGraphItemTimeDataList:[{
				XData:'2',
				XDateData:'2012-01-02',
				QCItemTime:{
					Date:'2012-01-02',
					EPBEquip_SName:'H7600',
					QCMat_SName:'QC1(H)',
					QCItem_ItemAllItem_SName:'ALT'
				}
			},{
				XData:'3',
				XDateData:'2012-01-03',
				QCItemTime:{
					Date:'2012-01-03'
				}
			},{
				XData:'1',
				XDateData:'2012-01-01',
				QCItemTime:{
					Date:'2012-01-01'
				}
			}]
		};
		return data;
	}
});