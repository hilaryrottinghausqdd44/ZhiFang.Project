/**
 * 历史对比
 * @author GUOHX
 * @version 2021-07-05
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		table = layui.table,
		$ = layui.jquery,
		form = layui.form;
	var app = {};
	//服务地址
	app.url = {
		//获得左侧列表数据   
		getLeftTableDataUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/NewLabStarResultMhistory',
		getresulthistory: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/LabStarResultHistory'
	};
	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.PATNO) {
			me.paramsObj.patno = params.PATNO;
		}
		if (params.CNAME) {
			me.paramsObj.CName = params.CNAME;
		}
		
		if (params.FIELDSLIST) {
			me.paramsObj.fieldsList = params.FIELDSLIST;
		}
		if (params.VALUELIST) {
			me.paramsObj.valueList = params.VALUELIST;
		}
		if (params.GTESTDATE) {
			me.paramsObj.GTestDate = params.GTESTDATE;
		}
	};
	//get参数
	app.paramsObj = {
		where: "",//传过来的查询条件	
		patno: "",
		GTestDate: "",
		CName: "",
		valueList: "",
		fieldsList:""
	};
	//初始化  
	app.init = function () {
		var me = this;		
		var url = location.search;//获取url中"?"符后的字串  		
		var str = url.substr(1);
		if (str) {
			this.getParams();
			var params = str.replace("where=", "");//where传来的条件有可能是1=1，此时如果用getParams方法获取的是where=1
			me.paramsObj.where = params.split("&")[0];
			//me.paramsObj.patno = params.split("&")[1].replace("patno=", "");
			//me.paramsObj.GTestDate = params.split("&")[2].replace("GTestDate=", "");
			//me.paramsObj.CName = params.split("&")[3].replace("CName=", "");
			me.initLeftTable();
			me.listeners();	
			me.loaddata();
		}
	};
	//初始化左侧列表  
	app.initLeftTable = function () {
		var me = this;	
		
		table.render({
			elem: '#historyTable',
			height: 'full-300',//table高度
			size: 'sm',
			page: false,//分页开启		
			url: "",
			data: [],
			cols: [],
			limit: 99999,
			limits: [10, 20, 50, 100, 200],
			autoSort: true, //禁用前端自动排序		
			text: {
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码	
					statusName: 'code', //code key	 
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态		
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.total || 0, //解析数据长度	
					"data": data.rows || []
				};
			},
			done: function (res, curr, count) {
				setTimeout(function () {
					if(count > 4){
						if ($("#historyTable+div .layui-table-body table.layui-table tbody tr:nth-child(4)")[0])
							$("#historyTable+div .layui-table-body table.layui-table tbody tr:nth-child(4)")[0].click();
					}
					else if (count > 0) {
						if ($("#historyTable+div .layui-table-body table.layui-table tbody tr:first-child")[0])
							$("#historyTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
					}
				}, 100);
				
			}
		});
		
	};
	app.loaddata = function () {
		var me = this;
		var cols = [];
		var url = me.url.getLeftTableDataUrl + "?Where=" + me.paramsObj.where + "&t=" + new Date().getTime();
		uxutil.server.ajax({
			url: url,
			async: true
		}, function (data) {
			if (data) {
				if (data.value) {
					for (var obj in data.value[0]) {
						if (obj == "ItemCname") {
							var col = { field: obj, minWidth: 100, title: "项目名称",hide:true };
							cols.push(col);
						} else if (obj == "itemno") {
							var col = { field: obj, minWidth: 100, title: "项目号",hide:true };
							cols.push(col);
						}else if (obj == "SampleInfo") {
							var col = { field: obj, minWidth: 100, title: "日期", fixed: 'left' };
							cols.push(col);
						}else if (obj == "SName") {
							var col = { field: obj, minWidth: 100, title: "简称", hide: true };
							cols.push(col);
						}
						else if (obj == "ItemDispOrder") {
							var col = { field: obj, minWidth: 100, title: "项目排序", hide: true };
							cols.push(col);
						}
						else {
							var col = { field: obj, minWidth: 100, title: data.value[0][obj] };//第一行数据为日期信息，作为列头显示
							cols.push(col);
						}
					}
					table.reload("historyTable", {
						url:'',
						cols: [cols],
						data: data.value.filter((a,idx)=>idx!=0)//第一行数据为日期信息，避免和列头重复，剔除掉
					})
				}
			} else {
				layer.msg(data.msg);
			}
		});
	};
	//监听事件	
	app.listeners = function () {
		var me = this;
		//监听左侧列表行单击事件
		table.on('row(historyTable)', function (obj) {
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			app.ResultHistoryInit(obj.data);
		});	 
	};
	app.ResultHistoryInit = function (obj) {
		var me = this;
		var fieldsList = me.paramsObj.fieldsList.split(",");//url里条件字段名集合
		var valueList = me.paramsObj.valueList.split(",");//url里条件字段值集合
		var otherWhere = "";//除了patno的其他条件
		//将传入的条件拼接到where条件里
		for (var i = 0; i < fieldsList.length;i++) {
			if (fieldsList[i] !="PatNo") {
				otherWhere += " and " + fieldsList[i] + "='" + valueList[i] + "'";
			}
			if (fieldsList[i] == "PatNo") {
				me.paramsObj.patno=  valueList[i];
			}
        }
		var url = me.url.getresulthistory + "?PatNo=" + me.paramsObj.patno + "&ItemNo=" + obj.itemno + "&Table=item&t=" + new Date().getTime();
		url += "&where= GTestDate >='" + uxutil.date.toString(uxutil.date.getNextDate(uxutil.date.getDate(me.paramsObj.GTestDate.split(" ")[0]), 1 - 60), true) + "' and  GTestDate <= '" + me.paramsObj.GTestDate.replace(/\//g,"-") + "'";  
		url += otherWhere;
		var data = null;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (sdata) {
			data = sdata;
		});
		var chartDom = document.getElementById('historyEchart');
		var myChart = echarts.init(chartDom);
		var option;

		option = {
			title: {
				text: '患者:' + me.paramsObj.CName + '(' + me.paramsObj.patno + ')' + '\n项目:' + obj.ItemCname + "(" + obj.SName+")",
				textStyle: {
					fontSize: 12
				}
			},
			tooltip: {
				trigger: 'axis',
			},
			legend: {
				data: []
			},
			toolbox: {
				show: true,
				feature: {
					dataZoom: {
						yAxisIndex: 'none'
					},
					dataView: { readOnly: false },
					magicType: { type: ['line', 'bar'] },
					restore: {},
					saveAsImage: {}
				}
			},
			xAxis: {
				type: 'category',
				boundaryGap: false,
				data: []
			},
			yAxis: {type: 'value'},
			series: [
				{
					name: '',
					type: 'line',
					data: [],
					markPoint: {
						symbolSize: [30, 30],//气泡大小
						itemStyle:{
							normal:{
								label:{
									fontSize: 10//气泡文字大小
								}
							}
						},
						data: [
							{ type: 'max', name: '最大值' },
							{ type: 'min', name: '最小值' }
						]
					},
					markLine: {
						data: [
							{ type: 'average', name: '平均值' }
						]
					}
				}
			]
		};

		if (data && data.value) {
			for (var i = 0; i < data.value.length; i++) {
				option.xAxis.data.push(data.value[i]['GTestDate'] );
				option.series[0].data.push(data.value[i]['ReportValue']);
			}
		}
		option && myChart.setOption(option);
	};
	 
	//初始化调用入口
	app.init();
});