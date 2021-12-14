/**
 * 清除打印次数
 * @author 王耀宗
 * @version 2021-5-25
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'element', 'table', 'laydate'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		table = layui.table,
		laydate = layui.laydate,
		$ = layui.jquery;
	var app = {};
	//服务地址
	app.url = {
		//
		selectReportUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/SelectReport?fields=PatNo,ClientPrint,PRINTTIMES,CNAME,SampleNo,CHECKDATE,CHECKTIME,RECEIVEDATE,SectionType,FormNo,ReportFormID,Serialno',
		//删除自助打印次数
		deleteClientPrintUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/deleteClientPrint',
		//删除临床打印次数
		deletePrintTimesUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/deletePrintTimes',
		//小组
		selectPGroupUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetPGroup?fields=SectionNo,CName'

	};

	app.cols = {
		left: [
			[
				{ type: 'checkbox' },
				{
					field: 'ReportFormID',
					title: 'ID',
					width: 100,
					//align: 'center',
					hide: true,
					sort: false
				},
				{
					field: 'FormNo',
					title: 'id',
					width: 100,
					//align: 'center',
					hide: true,
					sort: false
				},
				{
					field: 'CNAME',
					title: '姓名',
					width: 140,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: 'PatNo',
					title: '病历号',
					width: 250,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: 'Serialno',
					title: '条码号',
					width: 280,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: 'PRINTTIMES',
					title: '临床打印次数',
					width: 130,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: 'clientprint',
					title: '自助打印次数',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: '',
					title: '清除自助打印次数',
					width: 120,
					align: 'center',
					//hide: true,
					sort: false,
					toolbar: '#delClientPrintbar'
				},
				{
					field: '',
					title: '清除临床打印次数',
					width: 120,
					align: 'center',
					//hide: true,
					sort: false,
					toolbar: '#delPrintTimesbar'
				}
			]
		]
	};
	app.PGroupList = [];
	//初始化  
	app.init = function () {
		var me = this;

		me.initBottomTable("&where=(RECEIVEDATE=2021-01-01)");
		me.initPGroupSelect();
		me.listeners();
	};

	//监听事件
	app.listeners = function () {
		var me = this;
		//日期范围
		laydate.render({
			elem: '#selectdateValue'
			, range: '~'
		});
		//表格顶部工具栏监听
		table.on('toolbar(clearPrintCountTable)', function (obj) {
			// var checkStatus = table.checkStatus(obj.config.id);
			// var data = checkStatus.data;
			switch (obj.event) {
				case 'search':
					app.searchData();

					break;
			};
		});
		//监听表格工具条
		table.on('tool(clearPrintCountTable)', function (obj) {
			var data = obj.data;
			if (obj.event === 'delClientPrint') {
				layer.open({
					title: '删除确认'
					, content: '确定要删除吗！'
					, btn: ['确认', '取消']
					, skin: 'layui-layer-molv' //样式类名
					, yes: function (index, layero) {
						//确认按钮
						var entity = { formno: [data.FormNo] };
						uxutil.server.ajax({
							url: me.url.deleteClientPrintUrl,
							async: false,
							type: "post",
							data: JSON.stringify(entity)
						}, function (data) {
							if (data) {
								if (data.success) {
									layer.msg("删除成功");

								} else {
									layer.msg("删除失败");
								}

							} else {
								layer.msg("删除失败");
							}
						});
						app.searchData();
						layer.close(index); //如果设定了yes回调，需进行手工关闭
					}
					, btn2: function (index, layero) {

					}
				});
			} else if (obj.event === 'delPrintTimes') {
				layer.open({
					title: '删除确认'
					, content: '确定要删除吗！'
					, btn: ['确认', '取消']
					, skin: 'layui-layer-molv' //样式类名
					, yes: function (index, layero) {
						//确认按钮
						var entity = { formno: [data.FormNo] };
						uxutil.server.ajax({
							url: me.url.deletePrintTimesUrl,
							async: false,
							type: "post",
							data: JSON.stringify(entity)
						}, function (data) {
							if (data) {
								if (data.success) {
									layer.msg("删除成功");

								} else {
									layer.msg("删除失败");
								}

							} else {
								layer.msg("删除失败");
							}
						});
						app.searchData();
						layer.close(index); //如果设定了yes回调，需进行手工关闭
					}
					, btn2: function (index, layero) {

					}
				});
			}
		});
	}
	//查询按钮
	app.searchData = function () {
		var me = this;
		var where = "&where=( ";
		var selectdate = $("#selectdate").val();
		if (selectdate) {
			var CName = $("#CName").val();
			var SECTIONNO = $("#SECTIONNO").val();
			var SERIALNO = $("#SERIALNO").val();
			var PATNO = $("#PATNO").val();
			var selectdateValue = $("#selectdateValue").val();
			var info = me.isDateRangeValid(selectdateValue);
			if (info.msg != "") {
				layer.msg(info.msg);
				return;
			}
			where += selectdate + " >= '" + info.start + "' and " + selectdate + " <= '" + info.end + "' ";
			if (CName) {
				where += " and CName like '%" + CName + "%' ";
			}
			if (SECTIONNO) {
				where += " and SECTIONNO = '" + SECTIONNO + "' ";
			}
			if (SERIALNO) {
				where += " and SERIALNO = '" + SERIALNO + "' ";
			}
			if (PATNO) {
				where += " and PATNO = '" + PATNO + "' ";
			}
			where += " )";

			me.initBottomTable(where);
			//me.initBottomTable()执行过后，页面会刷新，所有条件要再初始化？
			laydate.render({
				elem: '#selectdateValue'
				, range: '~'
			});
			$("#selectdateValue").val(info.start + " ~ " + info.end);
			$("#CName").val(CName);
			$("#SERIALNO").val(SERIALNO);
			$("#SECTIONNO").val(SECTIONNO);
			$("#PATNO").val(PATNO);
			//选中某个选项后me.initBottomTable()执行过后，小组选择会变空，问题未知？
			if (me.PGroupList.length > 0) {
				var tempAjax = "";
				for (var i = 0; i < me.PGroupList.length; i++) {
					tempAjax = "<option value='" + me.PGroupList[i].SectionNo + "'>" + me.PGroupList[i].CName + "</option>";

					$("#SECTIONNO").append(tempAjax);
				}
				form.render('select'); //需要渲染一下;
			}
		} else {
			layer.msg("请选择时间，时间为必选项!");
		}
	}
	//初始化底部侧列表  
	app.initBottomTable = function (where) {
		var me = this;
		//page和limit默认会传
		var url = me.url.selectReportUrl + where;
		table.render({
			elem: '#clearPrintCountTable',
			height: 'full-5',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: url,
			toolbar: '#toolbarDemo', //开启头部工具栏，并为其绑定左侧模板
			defaultToolbar: [],
			cols: me.cols.left,
			limit: 50,
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


			}
		});
	};
	//初始化小组下拉框
	app.initPGroupSelect = function () {
		var me = this;
		var url = me.url.selectPGroupUrl;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (data) {
			if (data) {
				if (!data.success) {
					layer.msg(data.ErrorInfo);
					return;
				}
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
						//me[dataListName] = value.list;
					} else {
						value = value + "";
					}
				}
				if (!value) return;
				me.PGroupList = value;
				var tempAjax = "";
				for (var i = 0; i < value.length; i++) {
					tempAjax = "<option value='" + value[i].SectionNo + "'>" + value[i].CName + "</option>";
					$("#SECTIONNO").append(tempAjax);

				}
				form.render('select'); //需要渲染一下;			
			} else {
				layer.msg(data.msg);
			}
		});
	}
	//判断日期范围格式是否正确：2021-01-01 ~ 2021-02-01
	app.isDateRangeValid = function (DateRange) {
		var info = {};
		var msg = "";
		var start = "";
		var end = "";
		if (DateRange) {
			//验证日期是否正确
			if (DateRange.indexOf(" ~ ") == -1) {
				msg = "日期格式不正确!";
			} else {
				//验证是否都是日期
				var daterange = DateRange.split(" ~ ");
				if (daterange.length == 2) {
					start = daterange[0];
					end = daterange[1];
					var DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式
					if (!uxutil.date.isValid(start) || !DATE_FORMAT.test(start) || !uxutil.date.isValid(end) || !DATE_FORMAT.test(end)) {
						msg = "日期格式不正确!";
					}
					//验证开始日期是否大于结束日期
					//uxutil.date.difference()
					if (new Date(start).getTime() > new Date(end).getTime()) {
						msg = "开始日期不能大于结束日期!";
					}
					info.start = start;
					info.end = end;
				} else {
					msg = "日期格式不正确!";
				}
			}
		} else {
			msg = "日期不能为空!";
		}
		info.msg = msg;
		return info;
	};
	app.init();
});