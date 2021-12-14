/**
	@name：layui.ux.editortemplet 文本编辑器-模板
	@author：Jcall
	@version 2019-06-17
 */
layui.define(function(exports){
	"use strict";
	
	var editortemplet = {};
	
	//标题模板列表
	editortemplet.title = {
		"1":{
			"text":"标题",
			"memo":"备注",
			"content":
				'<div class="_editor editortitle">' +
					'<div style="text-align:center;margin-top:10px;margin-bottom:10px;">' +
						'<div style="border-width:6px;border-bottom-style:solid;border-color:#077e9a;display:inline-block;padding-right:5px;padding-left:5px;box-sizing:border-box;">' +
							'<p>' +
								'<strong>01</strong>' +
							'</p>' +
						'</div>' +
						'<div style="border-width:6px;border-bottom-style:solid;border-color:#66b6c3;display:inline-block;padding-right:10px;padding-left:10px;box-sizing:border-box;">' +
							'<p>' +
								'<strong>输入标题</strong>' +
							'</p>' +
						'</div>' +
					'</div>' +
				'</div>'
		},
		"2":{
			"text":"标题",
			"memo":"备注",
			"content":
				'<div class="_editor editortitle">' +
					'<div style="white-space:normal;margin:2px 0px;padding:0px;max-width:100%;border-top-style:solid;border-top-width:2px;border-color:rgb(99,135,146);border-left-width:0px;border-bottom-style:solid;border-bottom-width:2px;line-height:25px;color:rgb(99,135,146);font-weight:bold;text-align:center;">' +
						'<div style="border-color:rgb(99,135,146);color:inherit;">' +
							'<div style="border:0px rgb(99,135,146);text-align:center;margin:0.8em 0px 0.5em;box-sizing:border-box;padding:0px;color:inherit;">' +
								'<div style="color:inherit;display:inline-block;width:1.3em;font-size:5em;font-weight:inherit;line-height:1em;vertical-align:top;text-align:inherit;text-decoration:inherit;box-sizing:border-box;border-color:rgb(99,135,146);">' +
									'<div style="box-sizing:border-box;color:inherit;border-color:rgb(99,135,146);">04</div>' +
								'</div>' +
								'<div style="display:inline-block;margin-left:0.5em;margin-top:0px;text-align:left;box-sizing:border-box;color:inherit;border-color:rgb(99,135,146);">' +
									'<div style="box-sizing:border-box;color:inherit;border-color:rgb(99,135,146);">' +
										'<div style="margin-top:0px;box-sizing:border-box;color:inherit;display:inline-block;font-size:1.8em;font-weight:inherit;line-height:1.2;text-align:inherit;text-decoration:inherit;border-color:rgb(99,135,146);">' +
											'<span style="font-size:18px">LIST</span>' +
										'</div>' +
									'</div>' +
									'<div style="color:inherit;margin:0px;font-size:2em;line-height:1.4;font-weight:inherit;text-align:inherit;text-decoration:inherit;box-sizing:border-box;border-color:rgb(99,135,146);">' +
										'<p style="line-height:1.4;font-size:12px">No required</p>' +
										'<p style="line-height:1.4;font-size:12px">Free for commercial use</p>' +
									'</div>' +
								'</div>' +
							'</div>' +
						'</div>' +
					'</div>' +
				'</div>'
		}
	};
	
	//正文模板列表
	editortemplet.content ={
		
	};
	//节假日模板列表
	editortemplet.holiday ={
		"1":{
			"text":"端午",
			"memo":"备注",
			"content":
				'<div class="_editor editorholiday">' +
					'<div style="text-align: center;margin:10px 0px;">' +
						'<div style="display:inline-block;vertical-align: middle;width:25px;height:15px;background-color:#5ca690;transform: rotateZ(0deg);-webkit-transform: rotateZ(0deg);-moz-transform: rotateZ(0deg);-ms-transform: rotateZ(0deg);-o-transform: rotateZ(0deg);">' +
						'</div>' +
						'<div style="margin-right: -15px;margin-left: -15px;vertical-align: middle;display: inline-block;border-style: solid;border-width: 1px;border-color: #278e7f;padding: 5px 25px;border-radius: 5px;box-sizing: border-box;background-color:#fefefe;">' +
							'<p style="letter-spacing: 2px;">' +
								'<span style="font-size: 17px;color: #258855;">端午放假通知</span>' +
							'</p>' +
						'</div>' +
						'<div style="vertical-align: middle;display:inline-block;width:25px;height:15px;background-color:#5ca690;">' +
						'</div>' +
						'<div style="border-radius:8px;margin-top:-19px;border-style:solid;border-width:1px;border-color:#258855;padding:20px 15px 15px;">' +
							'<p style="text-align: left; letter-spacing: 2px;">' +
								'<span style="font-size: 14px; color: #258855;">经国务院批准现将端午节放假安排日期的具体安排通知如下：2019年6月7日(星期五)2019年至6月9日(星期日端午节)放假调休共3天。</span>' +
							'</p>' +
						'</div>' +
					'</div>' +
				'</div>'
		},
		"2":{
			"text":"端午",
			"memo":"备注",
			"content":
				'<div class="_editor editorholiday">' +
					'<div style="text-align: center;margin:10px 0px;">' +
						'<div style="border-left:solid;border-right:solid;border-bottom:solid;border-width:1px;border-color:#128770;">' +
							'<div style="display:inline-block;">' +
								'<div style="border-style:solid;border-width:1px;border-color:#128770;background-color:#fefefe;">' +
									'<div style="margin:3px 3px -3px -3px;border-style:solid;border-width:1px;border-color:#128770;background-color:#fdfdfd;">' +
										'<div style="margin:3px 3px -3px -3px;border-style:solid;border-width:1px;border-color:#128770;background-color:#ffffff;padding:0px 15px;">' +
											'<p style="letter-spacing:2px;">' +
												'<span style="color: #128770;">放假通知</span>' +
											'</p>' +
										'</div>' +
									'</div>' +
								'</div>' +
							'</div>' +
							'<div style="margin:20px 0px 10px;">' +
								'<div style="display: flex;justify-content:center;align-items: center;">' +
									'<div style="width:14%" data-width="14%">' +
										'<p><span style="color: #128770;">一</span></p>' +
										'<p><span style="color: #128770;">3</span></p>' +
									'</div>' +
									'<div style="width:14%" data-width="14%">' +
										'<p><span style="color: #128770;">二</span></p>' +
										'<p><span style="color: #128770;">4</span></p>' +
									'</div>' +
									'<div style="width:14%" data-width="14%">' +
										'<p><span style="color: #128770;">三</span></p>' +
										'<p><span style="color: #128770;">5</span></p>' +
									'</div>' +
									'<div style="width:14%" data-width="14%">' +
										'<p><span style="color: #128770;">四</span></p>' +
										'<p><span style="color: #128770;">6</span></p>' +
									'</div>' +
									'<div style="width:14%" data-width="14%">' +
										'<p><span style="color: #128770;">五</span></p>' +
										'<div style="background-color:#ffec17;display:inline-block;padding:0px 8px;border-radius:50%;">' +
											'<p><span style="color: #128770;">7</span></p>' +
										'</div>' +
									'</div>' +
									'<div style="width:14%" data-width="14%">' +
										'<p><span style="color: #128770;">六</span></p>' +
										'<div style="background-color:#ffec17;display:inline-block;padding:0px 8px;border-radius:50%;">' +
											'<p><span style="color: #128770;">8</span></p>' +
										'</div>' +
									'</div>' +
									'<div style="width:14%" data-width="14%">' +
										'<p><span style="color: #128770;">日</span></p>' +
										'<div style="background-color:#ffec17;display:inline-block;padding:0px 8px;border-radius:50%;">' +
											'<p><span style="color: #128770;">9</span></p>' +
										'</div>' +
									'</div>' +
								'</div>' +
							'</div>' +
							'<div style="margin:15px;">' +
								'<p style="text-align: left; letter-spacing: 2px;">' +
									'<span style="font-size: 14px; color: #128770;">经国务院批准现将端午节放假安排日期的具体安排通知如下：2019年6月7日(星期五)2019年至6月9日(星期日端午节)放假调休共3天。</span>' +
								'</p>' +
							'</div>' +
						'</div>' +
					'</div>' +
				'</div>'
		}
	};
	
	//暴露接口
	exports('uxeditortemplet',editortemplet);
});