/**
	@name：医院科室列表
	@author：guohaixiang
	@version 2019-12-12
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        uxtable = layui.uxtable;

    var config = "";
	var IsNode = false;
    var areaHospitalTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
            elem: '',
            id: "",
            checkRowData: [],
            /**默认传入参数*/
            defaultParams: {},
            /**默认数据条件*/
            defaultWhere: '',
            /**内部数据条件*/
            internalWhere: '',
            /**外部数据条件*/
            externalWhere: '',
            /**是否默认加载*/
            defaultLoad: false,
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',
            where: "",
            defaultToolbar: ['filter'],
            toolbar: '#areaHospitalToolbar',
            page: true,
            limit: 500,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            //size: 'sm', //小尺寸的表格
            cols: [[
                {
                    field: 'BHospital_Id',
                    width: 160,
                    title: '医院主键',
                    hide:true 
                },                             
                {
                    field: 'BHospital_Name',
                    title: '医院名称',
                    minWidth: 130
                    //hide:true 
                    //sort: true
                }
                
            ]],
            text: { none: '暂无相关数据' },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) {//res即为原始返回的数据
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
            	//子节点开启与关闭
            	if(IsNode){
            		$("#IsNode").next('.layui-form-switch').addClass('layui-form-onswitch');
                    $("#IsNode").next('.layui-form-switch').children("em").html("全节点");
                    $("#IsNode")[0].checked = true;
            	}else{
            		$("#IsNode").next('.layui-form-switch').removeClass('layui-form-onswitch');
                    $("#IsNode").next('.layui-form-switch').children("em").html("本节点");
                    $("#IsNode")[0].checked = false;
            	}
                //无数据处理
                if (count == 0) {
                    //layui.event("", "data", {  });
                    return;
                }
                //触发点击事件
                $("#areaHospitalTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, areaHospitalTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, areaHospitalTable, Class.pt, me);// table,
        return me;
    };
    Class.pt = Class.prototype;
    //获取查询Url
    Class.pt.getLoadUrl = function () {
        var me = this, arr = [];
        var url = me.config.selectUrl;
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
        //默认条件
        if (me.config.defaultWhere && me.config.defaultWhere != '') {
            arr.push(me.config.defaultWhere);
        }
        //内部条件
        if (me.config.internalWhere && me.config.internalWhere != '') {
            arr.push(me.config.internalWhere);
        }
        //外部条件
        if (me.config.externalWhere && me.config.externalWhere != '') {
            arr.push(me.config.externalWhere);
        }
        //传入的默认参数处理
        if (me.config.defaultParams) {

        }
        
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + JSON.stringify(where);
        }
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获取查询Fields
    Class.pt.getStoreFields = function (isString) {
        var me = this,
            columns = me.config.cols[0] || [],
            length = columns.length,
            fields = [];
        for (var i = 0; i < length; i++) {
            if (columns[i].field) {
                var obj = isString ? columns[i].field : {
                    name: columns[i].field,
                    type: columns[i].type ? columns[i].type : 'string'
                };
                fields.push(obj);
            }
        }
        return fields;
    };
    //核心入口
    areaHospitalTable.render = function (options,where) {
        var me = this;
        config=where;
        var inst = new Class(options);
        inst.config.defaultWhere = "AreaId='"+config+"'";
        inst.config.url = inst.getLoadUrl();
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    areaHospitalTable.onSearch = function (mytable, where) {
        var me = this;
        config=where;
        var inst = new Class(me);
        areaHospitalTable.url = inst.getLoadUrl();
        areaHospitalTable.elem = "#" + mytable;
        areaHospitalTable.id = mytable;
        table.reload(mytable, {
            url: inst.getLoadUrl()
        });
    };
    //判断浏览器大小方法
    Class.pt.screen = function ($) {
        //获取当前窗口的宽度
        var width = $(window).width();
        if (width > 1200) {
            return 3;   //大屏幕
        } else if (width > 992) {
            return 2;   //中屏幕
        } else if (width > 768) {
            return 1;   //小屏幕
        } else {
            return 0;   //超小屏幕
        }
    };
    Class.pt.iniListeners = function () {
        var me = this;
        //监听行单击事件
        table.on('row(areaHospitalTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            //layui.event("HospitalTableClick", "click", { id: obj.data.LBQCRuleBase_Id });
        });
        //table上面的工具栏
		table.on('toolbar(areaHospitalTable)', function(obj) {
			//console.log(me.config.checkRowData);
			switch(obj.event) {
				case 'add':
		            var flag = false;
		            parent.layer.open({
		                type: 2,
		                area: ['600px', '300px'],
		                fixed: false,
		                maxmin: false,
		                title: '区域新增医院',
                        content: uxutil.path.ROOT + '/ui/layui/views/system/Hospital/B_HospitalArea/areaHospitalForm/areaHospitalForm.html?AreaId=' + config +"&formType=add",
		                cancel: function (index, layero) {
		                    flag = true;
		                },
		                success: function (layero, index) {
		                    var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
		                    body.find('#AreaID').val(config);
		                },
		                end: function () {
		                    if (flag) return;
		                    table.reload(me.config.id,{
                            	url:me.getLoadUrl()
                            });
		                }
		            });
					break;
				case 'del':
					if(me.config.checkRowData.length === 0) {
						layer.msg('请选择一行！');
	               } else {
	                    layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
	                        var loadIndex = layer.load();//开启加载层
							var len = me.config.checkRowData.length;
							var fields="Id,AreaID";
						  	for(var i = 0; i < me.config.checkRowData.length; i++){
						  		var Id = me.config.checkRowData[i]["BHospital_Id"];
						  		var postData = {Id:Id,AreaID:null};
						  		$.ajax({
									type:'post',
									dataType:'json',
									contentType: "application/json",
									data:JSON.stringify({entity:postData,fields:fields}),
									url:uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalByField',
					                success: function (res) {
					                    layer.close(loadIndex);//关闭加载层
					                    layer.close(index);
										if(res.success){
											layer.msg("删除成功！",{icon: 6,anim:0});
                                            table.reload(me.config.id,{
                                            	url:me.getLoadUrl()
                                            });
										}else{
											layer.msg("删除失败！",{icon: 5,anim:6});
										}
									}
								});								
						  	}
						});
					}
					break;
			};
		});
		form.on('switch(IsNode)', function(data){
			IsNode = data.elem.checked;
		  //console.log(data.elem); //得到checkbox原始DOM对象
		  //console.log(data.elem.checked); //开关是否开启，true或者false
		  //console.log(data.value); //开关value值，也可以通过data.elem.value得到
		 // console.log(data.othis); //得到美化后的DOM对象
		 	if(data.elem.checked){
			 	 var loadIndex = layer.load();//开启加载层
			 		$.ajax({
						type:'get',
						dataType:'json',
						url:uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaSonByID?id='+config,
		                success: function (res) {
		                    layer.close(loadIndex);//关闭加载层
							if(res.success){
								me.config.defaultWhere = "";
								me.config.internalWhere = "AreaId in ("+res.ResultDataValue+")";
	                            table.reload(me.config.id,{
	                            	url:me.getLoadUrl()
	                            });
							}else{
								layer.msg("获取子节点医院失败！",{icon: 5,anim:6});
							}
						}
					});	
		 	}else{
		 		me.config.defaultWhere = "AreaId='"+config+"'";
		 		me.config.internalWhere = "";
		 		table.reload(me.config.id,{
                	url:me.getLoadUrl()
                });
		 	}
		});  
    };
    
    //暴露接口
    exports('areaHospitalTable', areaHospitalTable);
});
