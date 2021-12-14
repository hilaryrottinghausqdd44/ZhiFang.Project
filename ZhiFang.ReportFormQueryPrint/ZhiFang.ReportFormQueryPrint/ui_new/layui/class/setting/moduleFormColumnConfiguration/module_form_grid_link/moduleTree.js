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
            if (me.config.data != null) {
                
                        var tree_option = {
                            elem: options.elem,
                            id: options.id, //定义索引
                            data: me.config.data,
                            click: function (obj) {
                                if (obj.data.children == 0) {
                                    //设置选中节点样式
                                    $(options.elem + " span.layui-tree-txt").css("background-color", "#fff");
                                    $(obj.elem[0]).find("span.layui-tree-txt:first").css("background-color", "#b6f7f1");
                                    layui.event("treeClick", "click", { id: obj.data.id, name: obj.data.title });
                                };
                            }
                        };
                        
                        tree.render(tree_option);
                    
            }
            // if (me.config.data == null) {
            //     var task = setInterval(function () {
            //         if (me.config.data != null) {
            //             var tree_option = {
            //                 elem: options.elem,
            //                 id: options.id, //定义索引
            //                 data: me.config.data,
            //                 click: function (obj) {
            //                     if (obj.data.children == 0) {
            //                         //设置选中节点样式
            //                         $(options.elem + " span.layui-tree-txt").css("background-color", "#fff");
            //                         $(obj.elem[0]).find("span.layui-tree-txt:first").css("background-color", "#b6f7f1");
            //                         layui.event("treeClick", "click", { id: obj.data.id, name: obj.data.title });
            //                     };
            //                 }
            //             };
            //             clearInterval(task);
            //             task = null;
            //             return tree.render(tree_option);
            //         }
            //     }, 200);
            // }
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
        selectUrl: uxutil.path.LIIP_ROOT + '/ServiceWCF/RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID',
        selectUrl_client: uxutil.path.ROOT + '/ServiceWCF/CommonService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpIDByModuleID'
    };
    //获得树结构
    Class.pt.getTreeData = function (role) {
        var me = this;
        var list=[
            {
                "Tree": [
                    {
                        "Tree": null,
                        "text": "医生页面",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "5296815982421190629",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": [],
                        "text": "护士页面",
                        "expanded": true,
                        "leaf": false,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "2",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": null,
                        "text": "查询台页面",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "3",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": null,
                        "text": "技师站页面",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "4",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": null,
                        "text": "站点查询",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "6",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": null,
                        "text": "分库查询",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "7",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": null,
                        "text": "集中打印",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "8",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": null,
                        "text": "检验前后查询",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "9",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    {
                        "Tree": null,
                        "text": "LabStar调用",
                        "expanded": true,
                        "leaf": true,
                        "icon": "configuration.PNG",
                        "iconCls": null,
                        "url": "",
                        "tid": "10",
                        "pid": "4656964878417007884",
                        "objectType": null,
                        "value": null,
                        "Para": ""
                    },
                    
                ],
                "text": "模块关系设置",
                "expanded": true,
                "spread":true,//默认是否展开
                "leaf": false,
                "icon": "package.PNG",
                "iconCls": null,
                "url": "",
                "tid": "",
                "pid": "0",
                "objectType": null,
                "value": null,
                "Para": ""
            }
        ]
        var data = me.handleResultData(list);
        data = me.delTreeEmptyData(data);
        moduleTree.config.data = data;
        // var me = this,
        //     url = "";
        // if (role == "system") 
        //     url = me.config.selectUrl;
        // else 
        //     url = me.config.selectUrl_client;
        // var loadIndex = layer.open({ type: 3 });
        // $.ajax({
        //     type: "get",
        //     url: url,
        //     dataType: 'json',
        //     //async: false,
        //     success: function (res) {
        //         layer.close(loadIndex);
        //         if (res.success) {
        //             var data = [];
        //             if (res.ResultDataValue != "" && res.ResultDataValue != null) {
        //                 var Tree = JSON.parse(res.ResultDataValue).Tree;
        //                 data = me.handleResultData(Tree);
        //                 data = me.delTreeEmptyData(data);
        //             }
        //             moduleTree.config.data = data;
        //         } else {
        //             layer.msg("数组件数据获取失败！", { icon: 5, anim: 6 });
        //             moduleTree.config.data = [];
        //         }
        //     }
        // });
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
                equipModule: data[i].Para,
                children: children,
                spread:data[i].spread
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