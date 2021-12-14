/**
	@name：树组件
	@author：guohx
	@version 2020-07-29
 */
layui.extend({
}).define(['tree'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        tree = layui.tree;

    var moduleTree = {
        //全局项
        config: {
            PK: null,
            data: null
        },
        render: function (options) {
            var me = this;
            //获得树
            Class.pt.getTreeData(options.role);
            //初始化树
            if (me.config.data == null) {
                var task = setInterval(function () {
                    if (me.config.data != null) {
                        var tree_option = {
                            elem: options.elem,
                            id: options.id, //定义索引
                            data: me.config.data,
                            click: function (obj) {
                                if (obj.data.children == 0) {
                                	var codeConfigList = [];
                                	if(obj.data.ComponentsListJson){
                                		try{
                    						codeConfigList = JSON.parse(obj.data.ComponentsListJson);
                    						if(codeConfigList instanceof  Array) {
                    							//设置选中节点样式
			                                    $(options.elem + " span.layui-tree-txt").css("background-color", "#fff");
			                                    $(obj.elem[0]).find("span.layui-tree-txt:first").css("background-color", "#b6f7f1");
			                                    layui.event("treeClick", "click", { id: obj.data.id, name: obj.data.title ,codeList: codeConfigList,systemCode:obj.data.systemCode});
                    						}else {
                    							layer.msg('该模块配置不规范,请按照规范进行模块配置');
                    						}
                    						
                    					}catch(e){
                    						layer.msg('json解析异常，请检查功能配置模块的json是否按照规范书写！');
                    					}
                                		
                                	}else {
                                		layer.msg('该模块没有配置相关信息，请先配置后再进行操作！');
                                	}
                                };
                            }
                        };
                        clearInterval(task);
                        task = null;
                        return tree.render(tree_option);
                    }
                }, 200);
            }
        },
        //设置全局项
        set: function (options) {
            var me = this;
            me.config = $.extend({}, me.config, options);
            return me;
        }
    };
    //构造器
    var Class = function (setings) {
        var me = this;
        me.config = $.extend({}, me.config, moduleTree.config, setings);
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {
        PK: null,
        // 配置了的树的服务：开发树
        selectUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID',
        // 用户树，要调去平台的服务
        selectUrl_client: uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACModuleToTreeByWhere?where=(ComponentsListJson is not null)and (ComponentsListJson <>'')"
    };
    //获得树结构
    Class.pt.getTreeData = function (role) {
        var me = this,
            url = "";
        if (role == "system") 
            url = me.config.selectUrl;
        else 
            url = me.config.selectUrl_client;
        var loadIndex = layer.open({ type: 3 });
        $.ajax({
            type: "get",
            url: url,
            dataType: 'json',
            //async: false,
            success: function (res) {
                layer.close(loadIndex);
                if (res.success) {
                    var data = [];
                    if (res.ResultDataValue != "" && res.ResultDataValue != null) {
                    	var Tree = JSON.parse(res.ResultDataValue).Tree;
                        data = me.handleResultData(Tree);
                        if(role === "system"){
                        	data = me.delTreeEmptyData(data);
                        }
                    }
                    
                    moduleTree.config.data = data;
                } else {
                    layer.msg("数组件数据获取失败！", { icon: 5, anim: 6 });
                    moduleTree.config.data = [];
                }
            }
        });
    }
    //解析树返回数据
    Class.pt.handleResultData = function (data) {
        var me = this;
        var TreeData = [];
        for (var i = 0; i < data.length; i++) {
            var children = [];
            if (data[i].Tree != null && data[i].Tree.length > 0) {//存在下一级
                children = me.handleResultData(data[i].Tree);
            }
            var icon = '';
            if (children.length > 0) {
                icon = '<b class="layui-tree-iconArrow" style="position: absolute;top: 5px;left: -10px;"></b>';
            }
            var obj = {
                title: icon + data[i].text,  //一级菜单
                id: data[i].tid,
                systemCode: (data[i].url != null && data[i].url != 'null') ?data[i].url.split('/')[0].replace(/{|}/g,''):'',
                equipModule: data[i].Para,
                ComponentsListJson: data[i].ComponentsListJson,
                children: children
            };
            TreeData.push(obj);
        }
        return TreeData;
    }
    //树结构不存在下一级去除
    Class.pt.delTreeEmptyData = function (data) {
        var me = this;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].children.length == 0) {
                data.splice(i, 1);
            }
        }
        return data;
    }
    //暴露接口
    exports('moduleTree', moduleTree);
});