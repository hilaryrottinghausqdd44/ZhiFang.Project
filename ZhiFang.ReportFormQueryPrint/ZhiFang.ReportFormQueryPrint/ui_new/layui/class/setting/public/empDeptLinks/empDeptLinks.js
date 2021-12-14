/**
 * 全局设置
 * @author 王耀宗
 * @version 2021-6-17
 */


layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'form', 'table','transfer'], function () {
	var uxutil = layui.uxutil,
		form = layui.form,
		table = layui.table,
		transfer = layui.transfer,
		$ = layui.jquery;
	var app = {};
	app.clickUser={};//点击的某个用户
	app.userDeptList = [];//某一个用户拥有的科室列表
	app.userNotHaveDeptList = [];//某一个用户没有的科室列表
	app.url = {
		//获取用户列表
		selectUserUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetPUser',

		//用户对应科室
		selecDepttUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/GetEmpDeptLinks',
		//删除用户科室
    	deleteDeptUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/DeleteEmpDeptLinks',
		//科室表
		selectDeptListUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/GetDeptList?fields=CName,DeptNo',
		addUserDepttUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/AddEmpDeptLinks'
	};
	//get参数
	app.paramsObj = {
		ModuleID: '123',//模块id
		module: ''//模块
	};
	app.cols = {
		left: [
			[
				
				{
					field: 'UserNo',
					title: '用户编号',
					width: 100,
					//align: 'center',
					
					sort: false
				},
				{
					field: 'CName',
					title: '用户姓名',
					width: 100,
					//align: 'center',
					
					sort: false
				},
				{
					field: 'ShortCode',
					title: '账号',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: 'Password',
					title: '密码',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: 'Role',
					title: '角色',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				}
			]
		],
		right: [
			[
				{ type: 'checkbox' },
				{
					field: 'UserNo',
					title: '用户编号',
					width: 100,
					//align: 'center',
					
					sort: false
				},
				{
					field: 'UserCName',
					title: '用户姓名',
					width: 100,
					//align: 'center',
					
					sort: false
				},
				{
					field: 'ShortCode',
					title: '账号',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				},
				{
					field: 'DeptCName',
					title: '科室名称',
					width: 100,
					//align: 'center',
					//hide: true,
					sort: false
				},{
					field: 'EDLID',
					text: 'ID',
					width: 120,
					sortable: false,
					hide:true
				}
			]
		],
		dept: [
			[
				
				{
					field: 'DeptNo',
					title: '科室ID',
					width: 60,
					//align: 'center',
					
					sort: false
				},
				{
					field: 'CName',
					title: '科室名称',
					width: 100,
					//align: 'center',
					
					sort: false
				},
				
			]
		]
	};	
	app.selectUserWhere="";
	app.selectUserDeptWhere="";
	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		//获取模块id
		if (params.MODULEID) {
			me.paramsObj.ModuleID = params.MODULEID;
		}
		if (params.MODULE) {
			me.paramsObj.module = params.MODULE;
		}

	};
	//初始化  
	app.init = function () {
		var me = this;
		me.initUserTable("");
		me.initUserDeptTable("?where=(1=2)");
		// me.getParams();
		 me.listeners();
		// me.getGlobalSetting(me.paramsObj.module,"dataList");
		// me.getGlobalSetting("allPageType", "dataListDefult");//需要获取默认全局配置，获取ParaDesc
		// me.initHtml();
	};

	//监听事件
	app.listeners = function () {
		var me = this;
		//表格顶部工具栏监听
		table.on('toolbar(userTableLeft)', function (obj) {
			// var checkStatus = table.checkStatus(obj.config.id);
			// var data = checkStatus.data;
			switch (obj.event) {
				case 'search':
					var cname=$('#CName').val();
					if(!cname){
						me.initUserTable(""); 
					}else{
						me.selectUserWhere="?where=(CName='"+$('#CName').val()+"')";
						me.initUserTable(me.selectUserWhere); 
						$('#CName').val(cname);
					}
					break;
			};
		});
		//触发行单击事件
		table.on('row(userTableLeft)', function(obj){
			me.clickUser=obj.data;
			var cname=$('#CName').val();
			me.selectUserDeptWhere="?where=(UserNo = "+ obj.data.UserNo+")";
			me.initUserDeptTable(me.selectUserDeptWhere);
			$('#CName').val(cname);
		});
		//头工具栏事件
		table.on('toolbar(deptTableRight)', function(obj){
			var checkStatus = table.checkStatus(obj.config.id);
			var data = checkStatus.data;
			switch(obj.event){
			case 'addUserDept':
				
				if($.isEmptyObject(me.clickUser)){
					layer.msg("请选择某一个用户");
					return;
				}
				me.initAddUserDeptDiv();//初始化弹出框内容
				layer.open({
					title:'科室配置',
					type:1,
					content:$("#addUserDeptDiv"),
					area:['535px','513px'],//宽高
					skin: 'layui-layer-molv',//样式类名
					btn: ['保存'],
					yes:function(index, layero){
						var getData = transfer.getData('addUserDeptDiv'); //获取穿梭狂右侧数据
						if(getData.length==0){
							layer.msg("请添加一条数据");
							return;
						}
						var flag=me.addDepartment(getData);
						if(flag){
							layer.close(index);
							var cname=$('#CName').val();
							me.initUserDeptTable(me.selectUserDeptWhere);
							$('#CName').val(cname);
						}
						
					},
					end: function(){
						//form.val('dataForm',defaultData);
					}
				});
			break;
			case 'deleteUserDept':
				if(data.length<1){
					layer.open({
						title: '错误信息'
						, content: '请至少选择一条数据!'
						, skin: 'layui-layer-molv' //样式类名
					});
				}else{
					layer.open({
						title: '删除确认'
						, content: '确定要删除吗！'
						,btn:['确认','取消']
						, skin: 'layui-layer-molv' //样式类名
						,yes: function(index, layero){
							//确认按钮
							me.deleteDepartment(data);
							var cname=$('#CName').val();
							me.initUserDeptTable(me.selectUserDeptWhere);
							$('#CName').val(cname);
							layer.close(index); //如果设定了yes回调，需进行手工关闭
						}
						,btn2: function(index, layero){
						
						}
					});
					
				}
			break;
			};
		});
	}
	app.initUserTable = function (where) {
		var me = this;
		//page和limit默认会传
		var finalurl = me.url.selectUserUrl + where;
		table.render({
			elem: '#userTableLeft',
			height: 'full-7',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: finalurl,
			toolbar: '#toolbarUserLeft', //开启头部工具栏，并为其绑定左侧模板
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
					"count": data.count || 0, //解析数据长度	
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {


			}
		});
	};
	app.initUserDeptTable = function (where) {
		var me = this;
		//page和limit默认会传
		var finalurl = me.url.selecDepttUrl + where;
		table.render({
			elem: '#deptTableRight',
			height: 'full-7',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: finalurl,
			toolbar: '#toolbarDeptRight', //开启头部工具栏，并为其绑定左侧模板
			defaultToolbar: [],
			cols: me.cols.right,
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
				me.userDeptList=data.list || [];
				return {
					"code": res.success ? 0 : 1, //解析接口状态		
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度	
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				//赋值给公共变量，供添加某个用户的科室时判断是否拥有使用
				if (!res){
					me.userDeptList=[];
					return;
				} 
				
				me.userDeptList=res.data|| [];
				

			}
		});
	};
	app.initDeptListTable = function (where) {
		var me = this;
		//page和limit默认会传
		var finalurl = me.url.selectDeptListUrl + where;
		table.render({
			elem: '#deptListTable',
			height: 'full-7',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: finalurl,
			toolbar: '#toolbarDemo', //开启头部工具栏，并为其绑定左侧模板
			defaultToolbar: [],
			cols: me.cols.dept,
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
					"count": data.length || 0, //解析数据长度	
					"data": data || []
				};
			},
			done: function (res, curr, count) {


			}
		});
	};
	app.deleteDepartment=function(records){
		var me=this;
		var entityList=[];
		
		$.each(records,function(i,item){
			entityList.push({
                "EDLID": item.EDLID,
                "UserNo": item.UserNo,
                "UserCName": item.UserCName,
                "ShortCode": item.ShortCode,
                "DeptCName": item.DeptCName
            });
		})
		var dataList=JSON.stringify({entity:entityList});
		
		uxutil.server.ajax({
			url: me.url.deleteDeptUrl,
			async: false,
			type: "post",
			data: dataList
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
	}
	app.addDepartment=function(records){
		var me=this;
		var flag=false;
		var arr=[];
		$.each(records,function(i,item){
			arr.push({
                "EDLID": 0,
                "UserNo": me.clickUser.UserNo,
                "DeptNo": item.value,
                "UserCName": me.clickUser.CName,
                "ShortCode": me.clickUser.ShortCode,
                "DeptCName": item.title
            });
		})
		
		var entityList=JSON.stringify({entity:arr});
		
		uxutil.server.ajax({
			url: me.url.addUserDepttUrl,
			async: false,
			type: "post",
			data: entityList
		}, function (data) {
			if (data) {
				if (data.success) {
					layer.msg("添加成功");
					flag=true;
				} else {
					layer.msg(data.ErrorInfo);
				}

			} else {
				layer.msg("添加失败");
			}
		});
		return flag;
	}
	app.initAddUserDeptDiv=function(){
		
		var me=this;
		var where="&page=1&limit=100";
		if(me.userDeptList && me.userDeptList.length>0){
			
			var idlist=[];
			for(var i=0;i<me.userDeptList.length;i++){
				idlist.push(me.userDeptList[i].DeptNo)
			}
			where="&where= DeptNo not in ("+idlist.join()+")"+"&page=1&limit=100";
		}
		//me.initDeptListTable(where);
		var finalurl = me.url.selectDeptListUrl + where;
		uxutil.server.ajax({
			url: finalurl,
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
				me.userNotHaveDeptList=value || [];
			} else {
				layer.msg("aa");
			}
		});
		transfer.render({
			elem: '#addUserDeptDiv'
			,data: me.userNotHaveDeptList
			,title: ['待选列', '已选列']
			,showSearch: true
			,id:"addUserDeptDiv"
			,parseData: function(res){
				return {
				  "value": res.DeptNo //数据值
				  ,"title": res.CName //数据标题
				  
				}
			  }
			,text: {
				none: '无数据' //没有数据时的文案
				,searchNone: '无匹配数据' //搜索无匹配数据时的文案
			}  
		})
	}
	app.init();
});