//wx.config({
//  debug: true, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
//  appId: 'wx359def2eeed3abe6', // 必填，公众号的唯一标识
//  timestamp: 1414587457, // 必填，生成签名的时间戳
//  nonceStr: 'AAAA', // 必填，生成签名的随机串
//  signature: '',// 必填，签名，见附录1
//  jsApiList: ['scanQRCode'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
//});
/**
 * 获取token
 * 		https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx359def2eeed3abe6&secret=1a1e99b71a595ef3ae610e3b86fd0bc4
 * 获取ticket
 * 		https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=ACCESS_TOKEN&type=jsapi
 * 获取签名
 * 		jsapi_ticket=sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg&noncestr=Wm3WZYTPz0wzccnW&timestamp=1414587457&url=http://demo.zhifang.com.cn/zhifang.weixin/webapp/ui/home.html
 */
var shell_win_all = null;
$(function() {
	/**页面所有功能对象*/
	var shell_win = {
		/**用户信息*/
		user: {
			/**成员*/
			member: [{
				id: '1001',
				name: '张三',
				img: 'img/user/巴哥.jpg'
			}, {
				id: '1002',
				name: '李四',
				img: 'img/user/比格犬.jpg'
			}, {
				id: '1003',
				name: '王五',
				img: 'img/user/博美.jpg'
			}, {
				id: '1004',
				name: '赵六',
				img: 'img/user/茶杯犬.jpg'
			}]
		},
		/**按钮信息*/
		button: {
			/**最后一次点击的按钮*/
			last_button: null,
			/**按钮点击处理*/
			touch: function(but) {
				//改变样式
				if (this.last_button) {
					this.last_button.removeClass("page_home_floot_button_touch");
				}
				this.last_button = $(but);
				this.last_button.addClass("page_home_floot_button_touch");

				//定位到功能
				var id = but.id;
				switch (id) {
					case "button_home":
						shell_win.content.to_home();
						break;
					case "button_barcode":
						shell_win.content.to_barcord();
						break;
					case "button_report":
						shell_win.content.to_report();
						break;
					case "button_config":
						shell_win.content.to_config();
						break;
				}
			}
		},
		/**主页信息*/
		home: {

		},
		/**扫一扫*/
		barcode: {
			scan: function() {
				wx.scanQRCode({
					needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
					scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
					success: function(res) {
						var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果
						alert(result);
					}
				});
			}
		},
		/**报告查询*/
		report: {
			/**对象属性映射*/
			obj: {
				/**医院名称*/
				hospital_name: "hospital_name",
				/**检验单号*/
				check_list_number: "check_list_number",
				/**患者姓名*/
				patient_name: "patient_name",
				/**就诊时间*/
				visit_time: "visit_time",
				/**报告时间*/
				report_time: "report_time",
				/**列表字段*/
				list: "list",
				/**列表内容*/
				list_obj: {
					/**项目*/
					items: "items",
					/**结果*/
					result: "result",
					/**单位*/
					unit: "unit",
					/**参考值*/
					reference_value: "reference_value"
				}
			},
			/**最后一次头像*/
			last_img: null,
			/**最后一次数据行*/
			last_row: null,
			/**更新头像列表*/
			load_user_list: function(callback) {
				var member_div = this.get_member_list_div();
				callback(member_div);
			},
			/**获取成员列表*/
			get_member_list_div: function() {
				var member = shell_win.user.member,
					len = member.length,
					div = [],
					imgs = [];

				for (var i = 0; i < len; i++) {
					imgs.push(this.get_member_div_img(member[i]));
				}

				div.push('<div class="report_user_div">');
				div.push(this.get_barcode_div_img());
				div.push(imgs.join(""));
				div.push('</div>');

				div.push('<div id="report_grid_view"></div>');

				return div.join("");
			},
			/**获取条码图标*/
			get_barcode_div_img: function() {
				var div = [];

				div.push('<div id="user_img_barcode" class="user_picture">');
				div.push('<div class="circular_picture">');
				div.push('<figure><div><img src="img/icon/barcode.png"/></div></figure>');
				div.push('</div>');
				div.push('<span>条码</span>');
				div.push('</div>');

				return div.join("");
			},
			/**获取成员头像*/
			get_member_div_img: function(info) {
				var div = [];

				div.push('<div id="user_img_' + info.id + '" class="user_picture">');
				div.push('<div class="circular_picture">');
				div.push('<figure><div><img src="' + info.img + '"/></div></figure>');
				div.push('</div>');
				div.push('<span>' + info.name + '</span>');
				div.push('</div>');

				return div.join("");
			},
			/**联动处理*/
			do_link: function() {
				var member = shell_win.user.member,
					len = member.length,
					touch = Shell.util.Event.touch,
					link = [];

				var barcode = $("#user_img_barcode");
				barcode.on(touch, function() {
					shell_win_all.report.change_report_list_view("barcode");
					if (shell_win.report.last_img) {
						shell_win.report.last_img.removeClass("user_picture_touch");
					}
					barcode.addClass("user_picture_touch");
					shell_win.report.last_img = barcode;
				});
				for (var i = 0; i < len; i++) {
					var div = $("#user_img_" + member[i].id);
					div.on(touch, function() {
						var id = this.id.split("_").slice(-1);
						shell_win_all.report.change_report_list_view(id);
						if (shell_win.report.last_img) {
							shell_win.report.last_img.removeClass("user_picture_touch");
						}
						$(this).addClass("user_picture_touch");
						shell_win.report.last_img = $(this);
					});
				}
			},
			/**更改报告列表视图*/
			change_report_list_view: function() {
				this.get_report_list_from_server(function(data) {
					var view = "";
					if (data.success) {
						view = shell_win.report.get_report_list_view(data);
					} else {
						view = "<span style='color:red'>" + data.msg + "</span>";
					}
					$("#report_grid_view").html(view)
				});
			},
			/**获取报告列表视图*/
			get_report_list_view: function(data) {
				var rows = this.getRows(data),
					div = [];

				if (rows) {
					div.push(this.getRows(data));
					div.push(this.get_more_button());
				} else {
					div.push("没有查到记录");
				}

				return div.join("");
			},
			/**加载更多数据*/
			get_more_button: function() {
				var div = [];

				div.push('<div></div>');

				return div.join("");
			},
			/**改变数据*/
			getRows: function(data) {
				if (!data.success) return data;

				var value = data.value;
				value = typeof(value) === "string" ? $.decode(value) : value;

				var list = value.rows,
					length = list.length,
					divs = [];

				for (var i = 0; i < length; i++) {
					divs.push(this.get_row(list[i]));
				}

				return divs.join("");
			},
			/**创建数据行*/
			get_row: function(info) {
				var status = this.getSatus(info.status, info.pid),
					div = [];
				div.push('<div id="report_grid_row_' + info.id + '" class="report_grid_row" on' + Shell.util.Event.touch + '="shell_win_all.report.on_report_row_touch(' + info.id + ');">');
				div.push('<img class="report_grid_row_hospital_img" src="' + info.hospital_img + '"/>');
				div.push('<span class="report_grid_row_hospital_name">' + info.hospital_name + '</span>');
				div.push('<span class="report_grid_row_user_name">' + info.user_name + '</span>');
				div.push('<span class="report_grid_row_date">' + info.date + '</span>');
				div.push('<span class="report_grid_row_status ' + status.class + '">' + status.value + '</span>');
				div.push('<span class="report_grid_row_items">' + info.items + '</span>');
				div.push('</div>');
				return div.join("");
			},
			/**获取报告状态*/
			getSatus: function(status, id) {
				if (!id) return {
					value: "待出",
					class: "report_grid_row_status_has_not_out"
				};
				if (status) return {
					value: "已阅",
					class: "report_grid_row_status_has_readed"
				};
				return {
					value: "已出",
					class: "report_grid_row_status_has_out"
				};;
			},
			/**报告列表行点击*/
			on_report_row_touch: function(id) {
				if (shell_win.event.touch_start_x != shell_win.event.touch_end_x) return;
				var div_id = "report_grid_row_" + id,
					div = $("#" + div_id);

				if (shell_win.report.last_row) {
					shell_win.report.last_row.removeClass("report_grid_row_touch");
				}

				div.addClass("report_grid_row_touch");
				shell_win.report.last_row = div;
				shell_win.report.show_report_info(id);
			},
			/**显示报告内容*/
			show_report_info: function(id) {
				this.get_report_info_from_server(function(data) {
					var title = "报告单详情",
						view = "";
					if (data.success) {
						view = shell_win.report.get_report_info_view(data);
					} else {
						view = "<span style='color:red'>" + data.msg + "</span>";
					}
					shell_win.page.show_page_info(view, title);
				});
			},
			/**获取报告信息视图*/
			get_report_info_view: function(data) {
				var div = [],
					value = data.value;
				value = typeof(value) === "string" ? $.decode(value) : value;

				//出单医院
				div.push('<div class="report_title_div">');
				div.push(value[this.obj.hospital_name]);
				div.push('</div>');

				div.push('<div class="report_info_div">');
				//原始报告按钮
				div.push('<button class="report_info_div_button">原始报告</button>');
				//检验单号
				div.push('<span>检验单号:');
				div.push(value[this.obj.check_list_number]);
				div.push('</span></br>');
				//患者姓名
				div.push('<span>患者姓名:');
				div.push(value[this.obj.patient_name]);
				div.push('</span></br>');
				//就诊时间
				div.push('<span>:就诊时间');
				div.push(value[this.obj.visit_time]);
				div.push('</span></br>');
				//报告时间
				div.push('<span>报告时间:');
				div.push(value[this.obj.report_time]);
				div.push('</span>');
				div.push('</div>');

				//项目内容列表
				var list = value[this.obj.list],
					len = list.length,
					list_table = [];

				list_table.push('<table class="report_items_table">');
				list_table.push('<thead><th>检验项目</th><th>结果</th><th>单位</th><th>参考值</th></thead>');
				list_table.push('<tbody>');
				for (var i = 0; i < len; i++) {
					var obj = list[i];
					list_table.push('<tr>');
					list_table.push('<td>');
					list_table.push(obj[this.obj.list_obj.items]);
					list_table.push('</td>');
					list_table.push('<td>');
					list_table.push(obj[this.obj.list_obj.result]);
					list_table.push('</td>');
					list_table.push('<td>');
					list_table.push(obj[this.obj.list_obj.unit]);
					list_table.push('</td>');
					list_table.push('<td>');
					list_table.push(obj[this.obj.list_obj.reference_value]);
					list_table.push('</td>');
					list_table.push('</tr>');
				}
				list_table.push('</tbody>');
				list_table.push('</table>');

				div.push(list_table.join(""));

				return div.join("");
			},
			/**从服务器获取报告列表*/
			get_report_list_from_server: function(callback) {
				$.ajax({
					type: "get",
					dataType: "json",
					url: "server/get_report_list.txt",
					async: true,
					success: function(data, textStatus) {
						callback(data);
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						callback({
							success: false,
							msg: Shell.util.Error.getMsgByStatus(XMLHttpRequest.status)
						});
					}
				});
			},
			/**从服务器获取报告信息*/
			get_report_info_from_server: function(callback) {
				$.ajax({
					type: "get",
					dataType: "json",
					url: "server/get_report_info.txt",
					async: true,
					success: function(data, textStatus) {
						callback(data);
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						callback({
							success: false,
							msg: Shell.util.Error.getMsgByStatus(XMLHttpRequest.status)
						});
					}
				});
			}
		},
		/**设置*/
		config: {
			/**最后一个按钮*/
			last_button: null,
			/**获取设置页面*/
			grt_page: function() {
				var html = [];
				
				var list = Shell.util.UI.grid.createGrid({
					id:"content_config_buttons",
					data:[
						{text:"就诊人维护",id:"content_config_button_user"},
						{text:"就诊医院维护",id:"content_config_button_hospital"},
						{text:"就诊信息维护",id:"content_config_button_info"}
					],
					rowModel:'<div id="{id}" class="content_config_grid_row"><span>{text}</span></div>',
					hideLoadMore:true
				});
				return list;
				
//				html.push('<div id="content_config_buttons">');
//				
//				html.push('<div id="content_config_button_user" class="config_button">');
//				html.push('<span>就诊人</span>');
//				html.push('</div>');
//
//				html.push('<div id="content_config_button_hospital" class="config_button">');
//				html.push('<span>就诊医院</span>');
//				html.push('</div>');
//
//				html.push('<div id="content_config_button_info" class="config_button">');
//				html.push('<span>就诊信息</span>');
//				html.push('</div>');
//				
//				html.push('</div>');
//
//				return html.join("");
			},
			/**联动处理*/
			do_link: function() {
				$("#content_config_button_user").on(Shell.util.Event.touch, function() {
					shell_win.config.get_user_page();
				});
				$("#content_config_button_hospital").on(Shell.util.Event.touch, function() {
					shell_win.config.get_user_page();
				});
				$("#content_config_button_info").on(Shell.util.Event.touch, function() {
					shell_win.config.get_user_page();
				});
			},
			/**获取就诊人设置页面*/
			get_user_page: function() {
				this.get_user_list_from_server(function(data) {
					var view = "";
					if (data.success) {
						view = Shell.util.UI.grid.createGrid({
							id:"config_user_grid",
							data:data.rows
						});
					} else {
						view = "<span style='color:red'>" + data.msg + "</span>";
					}
					
					$("#content_config").apply(view);
				});
			},
			/**获取就诊医院设置页面*/
			get_hospital_page: function() {
				
			},
			/**获取就诊信息设置页面*/
			get_info_page: function() {
				var html = Shell.util.UI.grid.createGrid({
					id:"config_user_grid",
					data:""
				});
			},
			/**从服务器获取就诊人列表信息*/
			get_user_list_from_server: function(callback) {
				$.ajax({
					type: "get",
					dataType: "json",
					url: "server/get_report_info.txt",
					async: true,
					success: function(data, textStatus) {
						callback(data);
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						callback({
							success: false,
							msg: Shell.util.Error.getMsgByStatus(XMLHttpRequest.status)
						});
					}
				});
			}
		},
		/**内容区域信息*/
		content: {
			/**左后一次显示的内容*/
			last_content: null,
			/**显示保护层*/
			show_mark_div: function() {

			},
			/**隐藏保护层*/
			hide_mark_div: function() {

			},
			/**显示主页信息*/
			to_home: function() {
				var html = "建设中...";
				this.insert_content("content_home", html);

			},
			/**显示扫一扫*/
			to_barcord: function() {
				var html = "建设中...";
				this.insert_content("content_barcord", html);
			},
			/**显示报告查询*/
			to_report: function() {
				var div = $("#content_report");
				if (this.last_content) {
					this.last_content.hide();
				};
				if (div.length == 0) {
					shell_win.report.load_user_list(function(html) {
						shell_win.content.insert_content("content_report", html, shell_win.report.do_link);
					});
				} else {
					this.last_content = div;
					div.show();
				}
			},
			/**显示设置信息*/
			to_config: function() {
				var html = shell_win.config.grt_page();
				this.insert_content("content_config", html, shell_win.config.do_link);
			},
			/**添加内容*/
			insert_content: function(id, html, callback) {
				var div_id = "#" + id;
				if (this.last_content) {
					this.last_content.hide();
				};
				if ($(div_id).length == 0) {
					var parent = $("#page_home_content");

					var div =
						'<div id="' + id + '" style="padding :5px;background-color: #ffffff;">' +
						html +
						'</div>';

					parent.append(div);
					if (callback) callback();
				}

				this.last_content = $(div_id);
				$(div_id).show();
			}
		},
		/**页面信息*/
		page: {
			/**显示具体信息*/
			show_page_info: function(html, title) {
				$("#page_home").hide();

				var div = this.get_page_info_div(html, title);
				$("#page_info").html(div).show();
			},
			/**获取具体信息DIV*/
			get_page_info_div: function(html, title) {
				var div = [];

				div.push('<div class="navbar-fixed-top page_info_head">');
				div.push('<img src="img/icon/back.png" on' + Shell.util.Event.touch + '="shell_win_all.page.back_to_page_home();"></img>');

				if (title) {
					div.push('<span>');
					div.push(title);
					div.push('</span>');
				}

				div.push('</div>');
				div.push('<div class="page_info_content">');
				div.push(html);
				div.push('</div>');

				return div.join("");
			},
			/**回到主页面*/
			back_to_page_home: function() {
				$("#page_info").hide();
				$("#page_home").show();
			},
			/**回退页面*/
			back: function(page_now, back_to) {
				$(page_now).hide();
				$(back_to).show();
			},
			/**创建列表*/
			create_grid: function(id, html, callback) {
				var div = [];
				div.push('<div id="' + id + '">');
				div.push(html);
				div.push('<div id="' + id + '_load_more" class="grid_load_more_div"><span>加载更多</span></div>');
				div.push('</div>')

				return div.join("");
			},
			/**改变数据*/
			getRows: function(data) {
				if (!data.success) return data;

				var value = data.value;
				value = typeof(value) === "string" ? $.decode(value) : value;

				var list = value.rows,
					length = list.length,
					divs = [];

				for (var i = 0; i < length; i++) {
					divs.push(this.get_row(list[i]));
				}

				return divs.join("");
			},
		},
		/**动作*/
		event: {
			/**开始触摸点X位置*/
			touch_start_x: null,
			/**开始触摸点Y位置*/
			touch_start_y: null,
			/**结束触摸点X位置*/
			touch_end_x: null,
			/**结束触摸点Y位置*/
			touch_end_y: null
		},
		/**微信功能*/
		weixin: {

		},
		/**初始化*/
		init: function() {
			//功能按钮触摸处理
			$("#page_home_floot").children().each(function(but) {
				$(this).on(Shell.util.Event.touch, function() {
					shell_win.button.touch(this);
				});
			});
			//用户头像
			$("#user_image").attr("src", "img/img1.jpg");

			//			$("#page_info").on("touchstart", function(e) {
			//				var html = [];
			//				for(var i in e){
			//					html.push(i + "=" + e[i]);
			//				}
			//				$("#page_info").html(html.join("</br>"));
			//				
			//				shell_win.event.touch_start_x = e.pageX;
			//				shell_win.event.touch_start_y = e.pageY;
			//				shell_win.event.touch_end_x = e.pageX;
			//				shell_win.event.touch_end_y = e.pageY;
			//			}).on("touchmove", function(e) {
			//				shell_win.event.touch_end_x = e.pageX;
			//				shell_win.event.touch_end_y = e.pageY;
			//			}).on("touchend", function(e) {
			//				var x_move = shell_win.event.touch_end_x - shell_win.event.touch_start_x;
			//
			//				//向右滑动100个像素就返回到首页
			//				if (x_move > 100) {
			//					shell_win.page.back_to_page_home();
			//				}
			//			});
			//
			//			$("#page_home_content").on("touchstart", function(e) {
			//				shell_win.event.touch_start_x = e.pageX;
			//				shell_win.event.touch_start_y = e.pageY;
			//				shell_win.event.touch_end_x = e.pageX;
			//				shell_win.event.touch_end_y = e.pageY;
			//			}).on("touchmove", function(e) {
			//				shell_win.event.touch_end_x = e.pageX;
			//				shell_win.event.touch_end_y = e.pageY;
			//			});

			var page_info = document.getElementById("page_info");
			page_info.addEventListener("touchstart", function(e) {
				if (e.targetTouches.length == 1 && !self.busy) {
					var xy = e.targetTouches[0];
					shell_win.event.touch_start_x = xy.pageX;
					shell_win.event.touch_start_y = xy.pageY;
					shell_win.event.touch_end_x = xy.pageX;
					shell_win.event.touch_end_y = xy.pageY;
				}
			}, false);
			page_info.addEventListener("touchmove", function(e) {
				if (e.targetTouches.length == 1 && !self.busy) {
					e.preventDefault();
					var xy = e.targetTouches[0];
					shell_win.event.touch_end_x = xy.pageX;
					shell_win.event.touch_end_y = xy.pageY;
				}
			}, false);
			page_info.addEventListener("touchend", function(e) {
				var x_move = shell_win.event.touch_end_x - shell_win.event.touch_start_x;
				//向右滑动100个像素就返回到首页
				if (x_move > 100) {
					shell_win.page.back_to_page_home();
				}
			}, false);

			var page_home_content = document.getElementById("page_home_content");
			page_home_content.addEventListener("touchstart", function(e) {
				if (e.targetTouches.length == 1 && !self.busy) {
					var xy = e.targetTouches[0];
					shell_win.event.touch_start_x = xy.pageX;
					shell_win.event.touch_start_y = xy.pageY;
					shell_win.event.touch_end_x = xy.pageX;
					shell_win.event.touch_end_y = xy.pageY;
				}
			}, false);
			page_home_content.addEventListener("touchmove", function(e) {
				if (e.targetTouches.length == 1 && !self.busy) {
					var xy = e.targetTouches[0];
					shell_win.event.touch_end_x = xy.pageX;
					shell_win.event.touch_end_y = xy.pageY;
				}
			}, false);


			//定位功能
			var page_name = window.document.location.href.split("#")[1];
			switch (page_name) {
				case "home":
					shell_win.content.to_home();
					break;
				case "barcode":
					shell_win.content.to_barcord();
					break;
				case "report":
					shell_win.content.to_report()();
					break;
				case "config":
					shell_win.content.to_config();
					break;
			}
		}
	};
	//公开全局对象
	shell_win_all = shell_win;
	//初始化页面
	shell_win_all.init();
});