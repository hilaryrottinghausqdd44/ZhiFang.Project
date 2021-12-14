/**
 * CLodop打印底层插件
 * @author Jcall
 * @version 2020-12-07
 * @desc 首次批量直接打印存在问题，需要修正
 */

;!function(win){
	//模块对外公开名称
	var MOD_NAME = 'ZFPrint';
	//版本号
	var VERSION = '1.0.0.1';
	//默认打印任务名称
	var DEFAULT_TITLE = '智方打印任务';
	
	//CLodop组件
	var CLodop = {
		//CLodop组件需要引入的js文件,本地方式打印，两种端口
		LocalCLodopJs:[
			'http://localhost:8000/CLodopfuncs.js',
			'http://localhost:18000/CLodopfuncs.js',
			'http://127.0.0.1:8000/CLodopfuncs.js',
			'http://127.0.0.1:18000/CLodopfuncs.js'
		],
		//CLodop安装文件地址
		//LodopFiLeUrl:'Lodop6.226_Clodop4.113/CLodop_Setup_for_Win32NT_4.113Extend.exe',
		LodopFiLeUrl:'CLodop_Setup_for_Win64NT_4.127EN.exe',
		//获取本文件的文件夹地址
		_getPathUrl:function(){
			var scripts = document.getElementsByTagName('script'),
				scriptSrc = scripts[scripts.length-1].src,
				fileName = scriptSrc.split('/')[scriptSrc.split('/').length-1];
			return scriptSrc.replace(fileName,'');
		},
		//加载CLodop组件JS文件
		loadCLodopJs:function(callback){
			var me = this;
			if(me.isLoaded) return;
			me.isLoaded = true;
			
			var files = this.LocalCLodopJs;
			var body = document.body || document.getElementsByTagName("body")[0] || document.documentElement;
			for(var i in files){
				var oscript = document.createElement('script');
				oscript.type = 'text/javascript';
				oscript.src = files[i];
				
				body.appendChild(oscript);
			}
			
			callback && callback();
		}
	};
	//CLodop安装文件地址
	CLodop.LodopFiLeUrl = CLodop._getPathUrl() + CLodop.LodopFiLeUrl;
	
	//打印组件
	var ZFPrint = {
		//初始化打印组件
		init:function(options,callback){
			if(typeof(options) == 'function'){
				callback = options;
			}else{
				if(options && options.err){
					this.openFileUploadWindow = options.err;
				}
			}
			
			CLodop.loadCLodopJs(function(){
				setTimeout(function(){
					callback && callback();
				},100);
			});
		},
		//pdf打印
		pdf:{
			//直接打印:urls(pdf地址数组),title(打印任务名称,可选),callback(回调函数),isEncode(url是否转码,默认true)
			print:function(urls,title,callback,isEncode){
				this._todo(urls,title,function(){
					LODOP.PRINT();
					callback && callback();
				},isEncode);
			},
			//浏览打印:urls(pdf地址数组),title(打印任务名称,可选),callback(回调函数),isEncode(url是否转码,默认true)
			preview:function(urls,title,callback,isEncode){
				this._todo(urls,title,function(){
					LODOP.PREVIEW();
					//callback && callback();
				},isEncode);
			},
			//执行
			_todo:function(urls,title,callback,isEncode){
				var list = (urls instanceof Array) ? urls : [urls],
					title = title || (DEFAULT_TITLE + new Date().getTime());
					
				//url转码处理
				if(isEncode !== false){
					for(var i in list){
						list[i] = encodeURI(list[i]);
					}
				}
					
				ZFPrint._initLicenses(function(){
					LODOP.PRINT_INITA(0,0,"100%","100%",title);
					for(var i in list){
						LODOP.NEWPAGEA();
						LODOP.ADD_PRINT_PDF(0,0,"100%","100%",list[i]);
					}
					callback();
				});
			}
		},
		//模型打印
		model:{
			//直接打印:contentList(lodop内容字符串数组),title(打印任务名称,可选),callback(回调函数)
			print:function(contentList,title,callback){
				this._todo(contentList,title,function(){
					LODOP.PRINT();
					callback && callback();
				});
			},
			//浏览打印:contentList(lodop内容字符串数组),title(打印任务名称,可选),callback(回调函数)
			preview:function(contentList,title,callback){
				this._todo(contentList,title,function(){
					LODOP.PREVIEW();
					//callback && callback();
				});
			},
			//打印维护
			setup:function(content,title,callback){
				this._todo(content,title,function(){
					LODOP.PRINT_SETUP();
					//callback && callback();
				});
			},
			//打印设计
			design:function(content,title,callback){
				this._todo(content,title,function(){
					LODOP.PRINT_DESIGN();
					//callback && callback();
				});
			},
			//执行
			_todo:function(contentList,title,callback){
				var me = this,
					list = (contentList instanceof Array) ? contentList : [contentList],
					title = title || (DEFAULT_TITLE + new Date().getTime());
					
				ZFPrint._initLicenses(function(){
					LODOP.PRINT_INITA(0,0,"100%","100%",title);
					for(var i in list){
						LODOP.NEWPAGEA();
						var rowSource = list[i];
						var rowChange = '';
						try{
							rowChange = me._changeModelContent(rowSource);
							eval(rowChange);
						}catch(e){
							console && console.error('【转换前】',rowSource);
							console && console.error('【转换后】',rowChange);
							console && console.error(e);
						}
					}
					callback();
				});
			},
			//模板预览多页情况，清除任务设置指令
			_changeModelContent:function(content){
				//先使用换行符切分，再采用分号切分
				var arr = content.split('\r\n'),
					str = 'LODOP.PRINT_INIT',
					list = [];
				
				for(var i in arr){
					if(arr[i]){
						var rowArr = arr[i].split(';');
						for(j in rowArr){
							if(rowArr[j]){
								list.push(rowArr[j]+';');
							}
						}
					}
				}
				for(var i in list){
					if(list[i].trim().substr(0,str.length) == str){
						list[i] = '';
					}
				}
				
				return list.join('\r\n');
			}
		},
		//初始化Lodop注册信息
		_initLicenses:function(callback){
			var me = this;
			
			if(win.LODOP){
				if(typeof(LODOP.ADD_PRINT_PDF) == 'function'){
					//LODOP.SET_LICENSES("北京智方科技开发有限公司", "653726269717472919278901905623", "", "");
					LODOP.SET_LICENSES("智方（北京）科技发展有限公司","F8891E7BACFF708720795EB2B31ACDEED20","智方（北京）科技發展有限公司","B42575199843028F59C74AEBE781ED2DA61");
					LODOP.SET_LICENSES("THIRD LICENSE","","Zhifang (Beijing) Technology Development Co., Ltd","A5427B2406A3E024721029FE6FD4BE99BC7");
					callback();
				}else{
					me.openFileUploadWindow('2');
				}
			}else{
				me.openFileUploadWindow('1');
			}
		},
		//弹出下载CLodop文件弹框
		openFileUploadWindow:function(type){
			win.document.body.innerHTML = this.getMsgContent(type);
		},
		//下载安装文件提示内容
		getMsgContent:function(type){
			var me = this,
				msg = '';
				
			//type：1(未安装CLodop),2(CLodop版本不支持PDF打印)	
			switch (type){
				case '1': msg = '未安装CLodop打印软件，请安装支持PDF打印的CLodop软件!';break;
				case '2': msg = '该版本不支持PDF打印，请安装支持PDF打印的CLodop版本!';break;
				default: break;
			}
			
			var html = 
			'<div style="text-align:center;">' +
				'<font color="#FF00FF">' + msg + '<BR>' +
				'点击这里<a href="' + CLodop.LodopFiLeUrl + '" target="_self">' +
				'执行安装</a>,安装后请重新进入。</font>' +
			'</div>';
			
			return html;
		}
	};
	
	//公开的方法
	ZFPrint.getPublic = function(){
		return {
			"ZFPrint.init":"初始化打印组件",
			"ZFPrint.pdf.print":"pdf直接打印",
			"ZFPrint.pdf.preview":"pdf浏览打印",
			"ZFPrint.model.print":"模板直接打印",
			"ZFPrint.model.preview":"模板浏览打印",
			"ZFPrint.model.setup":"模板打印维护",
			"ZFPrint.model.design":"模板打印设计"
		}
	};
	
	//打印功能对外公开
	win[MOD_NAME] = ZFPrint;
}(window);