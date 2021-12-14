$(function() {
	//接收的参数
	var params = Shell.util.getRequestParams(true);
	//姓名
	var NAME = params.NAME;
	//病历号
	var PATNO = params.PATNO;
	
	//报告列表页面
	var report_grid_page = $("#report_grid_page");
	var report_grid_view = $("#report_grid_view");
	//报告信息页面
	var report_info_page = $("#report_info_page");
	var report_info_view = $("#report_info_view");
	var back_but = $("#back_but");
	
	//显示报告列表页面
	function showReportGridPage(){
		report_info_page.hide();
		report_grid_page.show();
	}
	//显示报告信息页面
	function showReportInfoPage(){
		report_grid_page.hide();
		report_info_page.show();
	}
	//初始化功能页面
	function init(){
		if(!NAME || !PATNO){
			ShellComponent.messagebox.msg("姓名和病历号不能为空！");
			return;
		}
		shell_win.report.get_html();//显示报告列表
	}
	//返回按钮监听
	back_but.on("click",function(){
		showReportGridPage();//显示报告列表页面
	});
	
	//页面所有功能对象
	var shell_win = {
		/**动作*/
		event: {
			/**开始触摸点X位置*/
			touch_start_x: null,
			/**开始触摸点Y位置*/
			touch_start_y: null,
			/**结束触摸点X位置*/
			touch_end_x: null,
			/**结束触摸点Y位置*/
			touch_end_y: null,
			/**X轴移动距离*/
			move_x:function(){
				if(shell_win.event.touch_end_x == null || 
					shell_win.event.touch_start_x == null) return null;
				return shell_win.event.touch_end_x - shell_win.event.touch_start_x;
			},
			/**Y轴移动距离*/
			move_y:function(){
				if(shell_win.event.touch_end_y == null || 
					shell_win.event.touch_start_y == null) return null;
				return shell_win.event.touch_end_y - shell_win.event.touch_start_y;
			},
			/**初始化*/
			init:function(){
				var page = window.document.body;
				page.addEventListener("touchstart", function(e) {
					if (e.targetTouches.length == 1 && !self.busy) {
						var xy = e.targetTouches[0];
						shell_win.event.touch_start_x = xy.pageX;
						shell_win.event.touch_start_y = xy.pageY;
						shell_win.event.touch_end_x = xy.pageX;
						shell_win.event.touch_end_y = xy.pageY;
					}
				}, false);
				page.addEventListener("touchmove", function(e) {
					if (e.targetTouches.length == 1 && !self.busy) {
						var xy = e.targetTouches[0];
						shell_win.event.touch_end_x = xy.pageX;
						shell_win.event.touch_end_y = xy.pageY;
					}
				}, false);
				page.addEventListener("touchend", function(e) {
					
				}, false);
			},
			/**是否是触摸*/
			is_touch:function(){
				var move_x = this.move_x(),
					move_y = this.move_y(),
					bo = true;
					
				if(move_x && (move_x > 10 || move_x < -10))
					bo = false;
					
				if(move_y && (move_y > 10 || move_y < -10))
					bo = false;
					
				return bo;
			}
		},
		/**报告查询*/
		report:{
			/**当前页签*/
			page:1,
			/**每页数量*/
			limit:20,
			/**缓存列表数据*/
			list:[],
			/**最后一次触摸的报告列表行*/
			last_report_id:null,
			/**对象属性映射*/
			obj: {
				/**属性字段*/
				info_field: "info",
				/**属性内容*/
				info_obj: {
					/**记录ID*/
					id: "id",
					/**报告ID*/
					report_id: "ReportId",
					/**报告状态*/
					report_status: "status",
					/**医院图片*/
					hospital_img: "hospital_img",
					/**医院名称*/
					hospital_name: "HospitalName",
					/**项目名称*/
					item_list_name: "item_list_name",
					/**检验单号*/
					check_list_number: "CheckListNumber",
					/**病历号*/
					patno:"PatNumber",
					/**患者ID*/
					patient_id: "PatientId",
					/**患者姓名*/
					patient_name: "PatientName",
					/**就诊时间*/
					visit_time: "VisitTime",
					/**报告时间*/
					report_time: "ReportTime",
					/**报告类型*/
					report_type:"ReportType",
					/**备注*/
					remarks:"ReportFORMMEMO"
				},
				/**生化物项目列表字段*/
				item_list_field: "list",
				/**生化物项目对象内容*/
				item_obj: {
					/**名称*/
					name: "ItemsName",
					/**结果*/
					result: "Result",
					/**描述*/
					desc:"ResultDesc",
					/**结果状态*/
					status:"ResultStatus",
					/**单位*/
					unit: "Unit",
					/**参考值*/
					reference_value: "ReferenceValue"
				},
				/**结果状态*/
				status:{
					"HH":{count:2,value:"超高",url:Shell.util.Path.uiPath + "/img/icon/up_red.png"},
					"H":{count:1,value:"偏高",url:Shell.util.Path.uiPath + "/img/icon/up_red.png"},
					"L":{count:1,value:"偏低",url:Shell.util.Path.uiPath + "/img/icon/down_red.png"},
					"LL":{count:2,value:"超低",url:Shell.util.Path.uiPath + "/img/icon/down_red.png"}
				},
				/**微生物项目列表字段*/
				micro_item_list_field:"MicroItemlist",
				/**微生物项目对象*/
				micro_item_obj:{
					/**项目名称*/
					name:"MicroItemName",
					MicroItemNo:"MicroItemNo",
					/**项目说明*/
					desc:"MicroItemDesc",
					/**过程描述数组*/
					DescList:"DescList",
					/**细菌列表字段*/
					micro_list_field:"MicroList",
					/**细菌对象内容*/
					micro_obj:{
						/**名称*/
						name:"MicroName",
						MicroNo:"MicroNo",
						/**备注*/
						remarks:"MicroDesc",
						/**药敏结果列表字段*/
						sensitivity_list_field:"AnitList",
						/**药敏结果对象内容*/
						sensitivity_obj:{
							/**药物名称*/
							name:"AnitName",
							/**类型*/
							type:"MethodName",
							/**结果值*/
							value:"SusQuan",
							/**药敏度*/
							result:"Suscept",
							RefRange:"RefRange"
						}
					}
				}
			},
			
			/**获取页面*/
			get_html:function(){
				shell_win.report.page = 1;//初始化到第一页
				shell_win.report.get_report_list(function(list){
					var html = shell_win.report.get_grid_html(list);
					
					if(!html){
						html = '<div class="report_grid_row" style="text-align:center;">没有报告</div>';
					}
						
					$("#report_grid_view").html(html);
					
					//显示报告列表页面
					showReportGridPage();
				});
			},
			/**加载更多数据*/
			on_load_more:function(){
				shell_win.report.page++;//加载下一页数据
				shell_win.report.get_report_list(function(list){
					var html = shell_win.report.get_grid_html(list);
					
					$("#report_row_load_more").remove();
					if(html.length > 0){
						$("#report_grid_view").append(html);
					}
				});
			},
			/**获取列表内容*/
			get_grid_html:function(list){
				var len = list.length,
					html = [];
				
				for(var i=0;i<len;i++){
					html.push('<div class="report_grid_row" id="report_row_');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id]);
					html.push('" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.report.on_report_row_touch(\'');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id]);
					html.push('\')">');
					
					html.push('<div class="report_grid_row_name">');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.hospital_name]);
					html.push('</div>');
					
					html.push('<div class="report_grid_row_right">〉</div>');
					
					html.push('<div class="report_grid_row_date">');
					html.push(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.visit_time]);
					html.push('</div>');
					
					html.push('</div>');
				}
					
				if(len == shell_win.report.limit){
					html.push('<div class="report_grid_row" id="report_row_load_more" style="text-align:center;" on');
					html.push(Shell.util.Event.touch);
					html.push('="shell_win_all.report.on_load_more()">');
					html.push('加载更多</div>');
				}
				return html.join("");
			},
			
			/**获取报告列表*/
			get_report_list:function(callback){
				ShellComponent.mask.loading();
				Shell.util.Server.ajax({
					url:Shell.util.Path.rootPath + 
					"/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetBSearchAccountRFListByPatNoAndName" + 
					"?PatNo=" + PATNO + "&Name=" + NAME + 
					'&Page=' + shell_win.report.page + '&limit=' + shell_win.report.limit
				},function(data){
					ShellComponent.mask.hide();
					var html = "";
					if(shell_win.report.list == 1){
						shell_win.report.list = [];
					}
					if(data.success){
						var list = data.value || [];
						
						shell_win.report.list = shell_win.report.list.concat(list);
						
						callback(list);
					}else{
						callback([]);
						ShellComponent.messagebox.msg(data.msg);
					}
				});
			},
			/**报告列表行触摸处理*/
			on_report_row_touch:function(id){
				var is_touch = shell_win.event.is_touch();
				if(!is_touch) return;
				
				if(shell_win.report.last_report_id){
					$("#report_row_" + shell_win.report.last_report_id).removeClass("button_touch");
				}
				shell_win.report.last_report_id = id;
				$("#report_row_" + id).addClass("button_touch");
				
				var list = shell_win.report.list,
					len = list.length,
					info = null;
					
				for(var i=0;i<len;i++){
					if(list[i][shell_win.report.obj.info_field][shell_win.report.obj.info_obj.report_id] == id){
						info = list[i];
						break;
					}
				}
				//加载报告信息
				shell_win.report.get_report_info(
					info[shell_win.report.obj.info_field][shell_win.report.obj.info_obj.patient_id],
					id,
					function(data){
						var html = [];
						for(var i=0;i<data.length;i++){
							html.push(shell_win.report.get_report_info_html(data[i]));
						}
						
						$("#report_info_view").html(html.join(""));
						showReportInfoPage();//显示报告信息页面
					}
				);
			},
			/**获取报告信息*/
			get_report_info:function(member_id,report_id,callback){
				ShellComponent.mask.loading();
				Shell.util.Server.ajax({
					url:Shell.util.Path.rootPath + "/ServerWCF/WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormListById",
					type:'post',
					data:Shell.util.JSON.encode({ReportFormIndexIdList:report_id})
				},function(data){
					ShellComponent.mask.hide();
					if(data.success){
						data.value[obj.info_obj.patient_name] = NAME;
						callback(data.value);
					}else{
						ShellComponent.messagebox.msg(data.msg);
					}
				});
			},
			/**获取报告信息页面内容*/
			get_report_info_html: function(data) {
				var obj = shell_win.report.obj;
					value = data || {},
					report_type = value[obj.info_field][obj.info_obj.report_type],
					html = 
						'<div style="margin:20px 10px;text-align:center;">' + 
							'不存在模板！报告类型：' + report_type + 
						'</div>';
					
				switch(report_type){
					case "Normal" : //生化类
					case "NormalIncImage" : //生化类（图）
						html = shell_win.report.normal_model.get_html(data);
						break;
						
					case "Micro" : //微生物
					case "MicroIncImage" : //微生物（图）
						html = shell_win.report.micro_model.get_html(data);
						break;
						
					case "CellMorphology" : //细胞形态学Marrow
					case "FishCheck" : //Fish检测（图）Marrow
					case "SensorCheck" : //院感检测（图）
					case "ChromosomeCheck" : //染色体检测（图）Marrow
					case "PathologyCheck" : //病理检测（图）Marrow
						break;
				}
					
				return html;
			},
			/**生化类报告模板*/
			normal_model:{
				/**获取内容*/
				get_html:function(data){
					var obj = shell_win.report.obj,
						value = data || {},
						info = value[obj.info_field] || {},
						has_status = false,
						div = [];
					
					//报告内容
					div.push('<div class="report_info_div">');
					//出单医院
					div.push('<div class="report_info_title_div">');
					div.push(info[obj.info_obj.hospital_name]);
					div.push('</div>');
					
					//检验单号
					//div.push('<div class="report_info_data_div">检验单号：');
					//div.push(info[obj.info_obj.check_list_number]);
					//div.push('</div>');
					div.push('<div class="report_info_data_div">病历号：');
					div.push(info[obj.info_obj.patno]);
					div.push('</div>');
					//患者姓名
					div.push('<div class="report_info_data_div">患者姓名：');
					div.push(info[obj.info_obj.patient_name]);
					div.push('</div>');
					//就诊时间
					div.push('<div class="report_info_data_div">就诊时间：');
					div.push(info[obj.info_obj.visit_time]);
					div.push('</div>');
					//报告时间
					div.push('<div class="report_info_data_div">报告时间：');
					div.push(info[obj.info_obj.report_time]);
					div.push('</div>');
					
					//原始报告
					//div.push('<div class="report_info_data_div">原始报告：');
					//div.push('<button onclick="shell_win_all.report.show_resouce_report_info();">原始报告</button>');
					//div.push('</div>');
					
					div.push('</div>');
					
					//项目内容列表
					var list = value[obj.item_list_field],
						len = list.length,
						list_table = [];
	
					list_table.push('<table class="report_items_table">');
					list_table.push('<thead><th>检验项目</th><th>结果</th><th>参考值</th></thead>');
					list_table.push('<tbody>');
					for (var i = 0; i < len; i++) {
						var row = list[i];
						list_table.push('<tr>');
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.name]);
						list_table.push('</td>');
						
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.result] || '');
						list_table.push(row[obj.item_obj.desc] || '');
						
						var status = row[obj.item_obj.status];
						if(status){
							has_status = true;
							var status_obj = obj.status[status];
							for(var m=0;m<status_obj.count;m++){
								list_table.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
								list_table.push(status_obj.url);
								list_table.push('"/>');
							}
						}
						
						list_table.push(' ' + (row[obj.item_obj.unit] || ''));
						list_table.push('</td>');
						
						list_table.push('<td>');
						list_table.push(row[obj.item_obj.reference_value]);
						list_table.push('</td>');
						list_table.push('</tr>');
					}
					list_table.push('</tbody>');
					list_table.push('</table>');
	
					div.push(list_table.join(""));
					
					//存在偏高偏低状态时显示备注信息
					if(has_status){
						var status = obj.status;
						div.push('<div class="report_info_note_div">');
						//第一行：偏高+超高
						div.push('<div style="font-size:0.8em;">备注:&nbsp;');
						div.push(status.H.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-right:10px;margin-top:-2px;" src="');
						div.push(status.H.url);
						div.push('"/>');
						
						div.push(status.HH.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
						div.push(status.HH.url);
						div.push('"/>');
						div.push('<img style="width:10px;height:10px;margin-right:10px;margin-top:-2px;" src="');
						div.push(status.HH.url);
						div.push('"/>');
		//				div.push('</div>');
		//				
		//				//第二行：偏低+超低
		//				div.push('<div style="style="font-size:0.8em;">');
						div.push(status.L.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-right:10px;margin-top:-2px;" src="');
						div.push(status.L.url);
						div.push('"/>');
						
						div.push(status.LL.value);
						div.push(':');
						div.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
						div.push(status.LL.url);
						div.push('"/>');
						div.push('<img style="width:10px;height:10px;margin-top:-2px;" src="');
						div.push(status.LL.url);
						div.push('"/>');
					}
						
					div.push('</div>');
	
					return div.join("");
				}
			},
			/**微生物报告模板*/
			micro_model:{
				/**获取内容*/
				get_html:function(data){
					var value = data || {},
						div = [];
						
					//报告信息内容
					div.push(this.get_info(value));
					//项目报告列表内容
					div.push(this.get_items(value));
					
					return div.join("");
				},
				/**报告信息内容*/
				get_info:function(value){
					var obj = shell_win.report.obj,
						info = value[obj.info_field] || {},
						div = [];
						
					//报告内容
					div.push('<div class="report_info_div">');
					
					//出单医院
					div.push('<div class="report_info_title_div">');
					div.push(info[obj.info_obj.hospital_name]);
					div.push('</div>');
					
					//检验单号
					div.push('<div class="report_info_data_div">检验单号：');
					div.push(info[obj.info_obj.check_list_number]);
					div.push('</div>');
					//患者姓名
					div.push('<div class="report_info_data_div">患者姓名：');
					div.push(info[obj.info_obj.patient_name]);
					div.push('</div>');
					//报告时间
					div.push('<div class="report_info_data_div">报告时间：');
					div.push(info[obj.info_obj.report_time]);
					div.push('</div>');
					
					//原始报告
					//div.push('<div class="report_info_data_div">原始报告：');
					//div.push('<button onclick="shell_win_all.report.show_resouce_report_info();">原始报告</button>');
					//div.push('</div>');
					
					div.push('</div>');
					
					return div.join("");
				},
				/**获取项目列表内容*/
				get_items:function(value){
					var obj = shell_win.report.obj,
						micro_item_obj = obj.micro_item_obj || {},
						list = value[obj.micro_item_list_field] || [],
						len = list.length,
						div = [];
	
					for(var i=0;i<len;i++){
						var item = list[i];
						//项目名称
						div.push('<div class="report_item_name_div">');
						div.push(item[micro_item_obj.name]);
//						div.push("&nbsp;");
//						div.push(item[micro_item_obj.desc]);
						div.push('</div>');
						//微生物列表
						div.push('<div class="report_item_content_div">');
						//描述
						div.push(this.get_desc(item[micro_item_obj.desc]));
						div.push(this.get_desc_list(item[micro_item_obj.DescList]));
						div.push(this.get_micro_list(item[micro_item_obj.micro_list_field]) || 
							item[micro_item_obj.micro_obj.remarks]);
						div.push('</div>');
					}
					
					return div.join("");
				},
				/**获取描述*/
				get_desc:function(value){
					var div = [];
					
					if(value){
						div.push('<div class="report_item_micro_info_div" style="background:orange;color:#ffffff;">');
						div.push(value);
						div.push('</div>');
					}
					
					return div.join("");
				},
				/**获取过程描述列表*/
				get_desc_list:function(list){
					var arr = list || [],
						len = arr.length,
						div = [];
						
					for(var i=0;i<len;i++){
						div.push('<div class="report_item_micro_info_div">');
						div.push(arr[i]);
						div.push('</div>');
					}
						
					return div.join("");
				},
				/**获取微生物列表内容*/
				get_micro_list:function(list){
					var obj = shell_win.report.obj,
						micro_obj = obj.micro_item_obj.micro_obj || {},
						list = list || [],
						len = list.length,
						div = [];
						
					for(var i=0;i<len;i++){
						var micro = list[i],
							sensitivity_list = micro[micro_obj.sensitivity_list_field] || [],
							sensitivity_exp = sensitivity_list.length == 0 ? "阴性" : "阳性";
						
						div.push('<div class="report_item_micro_info_div">');
						//微生物名称
						div.push('<div class="report_item_micro_info_title_div">');
						div.push(micro[micro_obj.name]);
						div.push('</div>');
//						//阴性/阳性
//						div.push('<span style="margin:0 10px;">');
//						div.push(sensitivity_exp);
//						div.push('</span>');
						//药敏结果列表
						div.push('<div class="report_item_micro_sensitivity_div">');
						//描述
						div.push(this.get_desc(micro[micro_obj.remarks]));
						div.push(this.get_sensitivity_list(sensitivity_list));
						div.push('</div>');
					}
					
					return div.join("");
				},
				/**获取药敏结果列表内容*/
				get_sensitivity_list:function(list){
					var obj = shell_win.report.obj,
						sensitivity_obj = obj.micro_item_obj.micro_obj.sensitivity_obj || {},
						list = list || [],
						len = list.length,
						div = [];
					
					div.push('<table class="report_item_micro_sensitivity_table">');
					div.push('<thead><th>药物(抗生素)</th><th>结果</th><th>药敏度</th></thead>');
					div.push('<tbody>');
					
					for(var i=0;i<len;i++){
						var row = list[i];
						div.push('<tr>');
						
						div.push('<td>');
						div.push(row[sensitivity_obj.name]);
						div.push('</td>');
						
						div.push('<td>');
						div.push(row[sensitivity_obj.value]);
						div.push(' (');
						div.push(row[sensitivity_obj.type]);
						div.push(')');
						div.push('</td>');
						
						div.push('<td>');
						div.push(row[sensitivity_obj.result]);
						div.push('</td>');
						
						div.push('</tr>');
					}
					
					div.push('</tbody></table>');
					
					return div.join("");
				}
			}
		}
	};
	//初始化页面动作
	shell_win.event.init();
	//公开全局对象
	shell_win_all = shell_win;
	
	//初始化功能页面
	init();
});