/**
   @Name：检验主界面
   @Author：zhangda
   @version 2021-04-01
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base',
    msgintegrator: 'modules/msg/integrator',
    tableSelect: '../src/tableSelect/tableSelect'
}).use(['uxutil','uxbase', 'element', 'form', 'layer','msgintegrator','tableSelect'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        msgintegrator = layui.msgintegrator,
        tableSelect = layui.tableSelect,
        uxbase = layui.uxbase,
        uxutil = layui.uxutil;

    //外部参数
    var PARAMS = uxutil.params.get(true);
    //模块ID
    var TABMODULEID = PARAMS.MODULEID;

    var app = {};

    //服务地址
    app.url = {
        GetSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true',
        LOGIN_URL: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login'//登录服务
    };
    //配置
    app.config = {
        userID: null,//登录人ID
        moduleJson: null,//模块配置JSON
        localTotalName: 'LabStar_TS',// localStorage中存储小组的名称
        localSectionName: 'OpenedSectionList',// localStorage中存储小组的名称
        activeTab: 0,//当前激活页签 //存储的是lay-id 也就是小组id 全部是0
        activeTabOrder: [0],//激活页签顺序 //存储的是lay-id 也就是小组id 全部是0
        iframeHeight: '100%',//($(window).height() - 50) + "px"
        iframeScrolling: 'yes', //iframe 滚动条
        //消息集成器
        isOpenMsgintegrator: true,//是否开启消息集成器
        loadTime: 5000,//刷新间隔时间 - 默认5秒
        loadSection: {},//记录需要刷新的小组及上传数据  格式：{ "小组ID":[样本单ID,样本单ID,……] }
        //危急值
        isOpenCV: true,//是否开启危急值
        CVLockLayerIndex: null,//危急值锁屏弹出层索引
        //列表表单配置 -- 未配置则采用通用配置
        defaultSectionType:'0'
    };

    //初始化
    app.init = function () {
        var me = this;
        me.config.userID = uxutil.cookie.get(uxutil.cookie.map.USERID);
        me.config.moduleJson = uxutil.params.getComponentsListJson() || null;
        me.config.moduleJson = me.config.moduleJson ? $.parseJSON(me.config.moduleJson) : null;
        me.setDomHeight();
        //加载iframe
        setTimeout(function () {
            var IUrl = uxutil.path.ROOT + '/ui/layui/views/sample/index.html?TABMODULEID=' + TABMODULEID;
            var html = '<iframe src="' + IUrl + '" id="IframeAllSection" name="IframeAllSection" frameborder="0" scrolling="' + me.config.iframeScrolling + '" marginheight="0" marginwidth="0" width="100%" height="' + me.config.iframeHeight + '">您的浏览器不支持嵌入式框架，或者当前配置为不显示嵌入式框架。</iframe >';
            $("#AllSection").html(html);
            $("#IframeAllSection").focus();//焦点定位到全部中
            $("#IframeAllSection")[0].onload = function () {
                $("#IframeAllSection")[0]["contentWindow"].MODULEJSONOBJ = me.getModuleConfigJsonBySectionType();
            };
        }, 0);
        me.getLocal();
        me.initSectionTableSelect();
        me.initListeners();
        if (me.config.isOpenMsgintegrator) me.initMsgintegrator();
        //setTimeout(function () { me.onCVTip([1,2]); },2000);
    };
    //设置dom元素高度
    app.setDomHeight = function () {
        var me = this;
        //设置iframe父元素高度
        $(".layui-tab-content").css("height", ($(window).height() - 32) + "px");
    };
    //监听
    app.initListeners = function () {
        var me = this;
        var iTime = null;//定时器
        // 窗体大小改变时，调整高度显示
        $(window).resize(function () {
            clearTimeout(iTime);
            iTime = setTimeout(function () {
                me.setDomHeight();
            }, 500);
        });
        //关闭小组
        $("#Tab").on('click', '.layui-icon-close', function () {
            layui.stope(window.event);//阻止默认事件
            var layid = $(this).parent('li').attr('lay-id');//lay-id 中存储的是sectionID
            //如果是当前激活页签 则切换到上一个激活页签
            element.tabDelete('Tab', layid);
            //删除记录的激活页签
            $.each(me.config.activeTabOrder, function (i,item) {
                if (item == layid) {
                    me.config.activeTabOrder.splice(i, 1);
                    return false;
                }
            });
            if (me.config.activeTab == layid) {
                element.tabChange('Tab', me.config.activeTabOrder[me.config.activeTabOrder.length - 1]);
            }
            //重新初始化 不然url不变
            me.initSectionTableSelect();
            //删除local记录
            me.delLocal(layid);
        });
        //新增小组 - 阻止选中li
        $("#addSection").on('click', function () {
            return false;
        });
        //监听小组页签切换
        element.on('tab(Tab)', function (data) {
            var layid = $(this).attr("lay-id"),
                index = null;
            //焦点定位到当前的iframe
            if (layid == 0)
                $("#IframeAllSection").focus();
            else
                $("#Iframe" + layid).focus();
            if (typeof layid == 'undefined' || layid == me.config.activeTab) return;
            //切换记录的顺序
            $.each(me.config.activeTabOrder, function (i, item) {
                if (item == layid) {
                    index = i;
                    return false;
                }
            });
            if (index != null) me.config.activeTabOrder.splice(index, 1);
            me.config.activeTabOrder.push(layid);
            me.config.activeTab = layid;
            //当前小组刷新处理
            me.onEquipResultMsgRefresh(layid);
            //当前小组界面高度重新处理
            me.onInterfaceSetDomHeight(layid);
        });
        //消息测试
        //$("#EquipResultMsgTest").click(function () {
        //    //发送消息测试
        //    var loadIndex = layer.load();//开启加载层
        //    uxutil.server.ajax({
        //        type: "get",
        //        //data:JSON.stringify({
        //        //	MessageInfo:"安徽省几点回家as回到家阿萨德",
        //        //	MessageType:"123",
        //        //	FormUserEmpId:"5239146706708216415",
        //        //	FormUserEmpName:"测试人员"
        //        //}),
        //        url: uxutil.path.ROOT + '/ServerWCF/TestService.svc/SendCommMessages?MessageInfo=1&MessageType=1&FormUserEmpId=5254139245102457908&FormUserEmpName=test1',
        //    }, function (data) {
        //        layer.close(loadIndex);//关闭加载层
        //        layer.msg("上传成功");
        //    });
        //});
    };
    //初始化新增小组列表
    app.initSectionTableSelect = function () {
        var me = this,
            checkedIds = [],
            url = me.url.GetSectionUrl + "&sort=[{property:'LBRight_LBSection_DispOrder',direction:'ASC'}]&where=lbright.RoleID is null and lbright.EmpID=" + me.config.userID;
        $.each(me.config.activeTabOrder, function (i, item) {
            if (item != 0) checkedIds.push(item);
        });
        if (checkedIds.length > 0) url += ' and lbright.LBSection.Id not in(' + checkedIds.join() + ')';
        url += "&fields=LBRight_LBSection_Id,LBRight_LBSection_CName,LBRight_LBSection_UseCode,LBRight_LBSection_SectionTypeID,LBRight_LBSection_SectionType,LBRight_LBSection_ProDLL,LBRight_LBSection_DispOrder";
        tableSelect.render({
            elem: '#addSectionBtn',	//定义输入框input对象 必填
            checkedKey: 'LBRight_LBSection_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbright.LBSection.CName,lbright.LBSection.UseCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '小组名称/编码',	//搜索输入框的提示文字 默认关键词搜索
            isHasCloseBtn: true,//是否存在关闭按钮
            isHideClearBtn: true,//是否隐藏清空按钮
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'checkbox' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBRight_LBSection_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBRight_LBSection_CName', width: 200, title: '小组名称', sort: false },
                    { field: 'LBRight_LBSection_UseCode', width: 100, title: '小组编码', sort: false },
                    { field: 'LBRight_LBSection_SectionTypeID', width: 100, title: '小组类型', sort: false,hide:true },
                    { field: 'LBRight_LBSection_SectionType', width: 100, title: '小组类型', sort: false },
                    { field: 'LBRight_LBSection_ProDLL', width: 100, title: '专业编辑', sort: false, hide: true },
                    { field: 'LBRight_LBSection_DispOrder', width: 80, title: '排序', sort: false }
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
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    $.each(data.data, function (i, item) {
                        var IUrl = uxutil.path.ROOT + '/ui/layui/views/sample/index.html?sectionID=' + item["LBRight_LBSection_Id"] + '&sectionCName=' + item["LBRight_LBSection_CName"] + '&sectionProDLL=' + item["LBRight_LBSection_ProDLL"] + '&TABMODULEID=' + TABMODULEID;
                        var html = '<iframe src="' + IUrl + '" id="Iframe' + item["LBRight_LBSection_Id"] + '" name="Iframe' + item["LBRight_LBSection_Id"] + '" frameborder="0" scrolling="' + me.config.iframeScrolling + '" marginheight="0" marginwidth="0" width="100%" height="' + me.config.iframeHeight + '">您的浏览器不支持嵌入式框架，或者当前配置为不显示嵌入式框架。</iframe >';
                        var titleDom = '<li lay-id="' + item["LBRight_LBSection_Id"] + '"><i class="iconfont">&#xe654;</i>&nbsp;' + item["LBRight_LBSection_CName"]+' <i class="layui-icon layui-icon-close"></i></li>';
                        var contentDom = '<div class="layui-tab-item" style="height:100%">' + html + '</div>';
                        $("#addSection").before($(titleDom));
                        $("#Tab .layui-tab-content").append(contentDom);
                        $("#Iframe" + item["LBRight_LBSection_Id"])[0].onload = function () {
                            $("#Iframe" + item["LBRight_LBSection_Id"])[0]["contentWindow"].MODULEJSONOBJ = me.getModuleConfigJsonBySectionType(item["LBRight_LBSection_SectionTypeID"]);
                        };
                        me.config.activeTabOrder.push(item["LBRight_LBSection_Id"]);
                        me.addLocal(item["LBRight_LBSection_Id"], item["LBRight_LBSection_CName"], item["LBRight_LBSection_SectionTypeID"], item["LBRight_LBSection_ProDLL"]);
                    });
                    element.tabChange('Tab', me.config.activeTabOrder[me.config.activeTabOrder.length - 1]);
                    me.config.activeTab = me.config.activeTabOrder[me.config.activeTabOrder.length - 1];
                    element.init();
                    //重新初始化 不然url不变
                    me.initSectionTableSelect();
                }
            }
        });
    };
    //获得local中的小组
    app.getLocal = function () {
        var me = this;
        var local = uxutil.localStorage.get(me.config.localTotalName, true);
        if (local) {
            if (local[me.config.userID]) {//存在当前等录人记录
                if (local[me.config.userID][me.config.localSectionName] && local[me.config.userID][me.config.localSectionName].length > 0) {//local中存储打开的小组
                    $.each(local[me.config.userID][me.config.localSectionName], function (i, item) {
                        var IUrl = uxutil.path.ROOT + '/ui/layui/views/sample/index.html?sectionID=' + item["Id"] + '&sectionCName=' + item["Name"] + '&sectionProDLL=' + item["sectionProDLL"] + '&TABMODULEID=' + TABMODULEID;
                        var html = '<iframe src="' + IUrl + '" id="Iframe' + item["Id"] + '" name="Iframe' + item["Id"] + '" frameborder="0" scrolling="' + me.config.iframeScrolling + '" marginheight="0" marginwidth="0" width="100%" height="' + me.config.iframeHeight + '">您的浏览器不支持嵌入式框架，或者当前配置为不显示嵌入式框架。</iframe >';
                        var titleDom = '<li lay-id="' + item["Id"] + '"><i class="iconfont">&#xe654;</i>&nbsp;' + item["Name"] + ' <i class="layui-icon layui-icon-close"></i></li>';
                        var contentDom = '<div class="layui-tab-item" style="height:100%">' + html + '</div>';
                        $("#addSection").before($(titleDom));
                        $("#Tab .layui-tab-content").append(contentDom);
                        $("#Iframe" + item["Id"])[0].onload = function () {
                            $("#Iframe" + item["Id"])[0]["contentWindow"].MODULEJSONOBJ = me.getModuleConfigJsonBySectionType(item["sectionTypeID"]);
                        };
                        me.config.activeTabOrder.push(item["Id"]);
                    });
                    element.tabChange('Tab', me.config.activeTabOrder[me.config.activeTabOrder.length - 1]);
                    me.config.activeTab = me.config.activeTabOrder[me.config.activeTabOrder.length - 1];
                    element.init();
                }
            }
        }
    };
    //新增小组加到local中
    app.addLocal = function (sectionID, sectionName, sectionTypeID, sectionProDLL) {
        var me = this,
            local = uxutil.localStorage.get(me.config.localTotalName, true);
        if (!sectionID || !sectionName) return;
        if (local) {
            if (local[me.config.userID]) {//存在当前等录人记录
                if (local[me.config.userID][me.config.localSectionName] && local[me.config.userID][me.config.localSectionName].length > 0) {//local中存储打开的小组
                    var flag = false;//是否存在
                    $.each(local[me.config.userID][me.config.localSectionName], function (i, item) {
                        if (item["Id"] == sectionID) {
                            flag = true;
                            return false;
                        }
                    });
                    if (!flag) //不存在加入
                        local[me.config.userID][me.config.localSectionName].push({ Id: sectionID, Name: sectionName, sectionTypeID: sectionTypeID, sectionProDLL: sectionProDLL });
                } else {
                    local[me.config.userID][me.config.localSectionName] = [];
                    local[me.config.userID][me.config.localSectionName].push({ Id: sectionID, Name: sectionName, sectionTypeID: sectionTypeID, sectionProDLL: sectionProDLL });
                }
            } else {
                local[me.config.userID] = {};
                local[me.config.userID][me.config.localSectionName] = [];
                local[me.config.userID][me.config.localSectionName].push({ Id: sectionID, Name: sectionName, sectionTypeID: sectionTypeID, sectionProDLL: sectionProDLL });
            }
        } else {
            local = {};
            local[me.config.userID] = {};
            local[me.config.userID][me.config.localSectionName] = [];
            local[me.config.userID][me.config.localSectionName].push({ Id: sectionID, Name: sectionName, sectionTypeID: sectionTypeID, sectionProDLL: sectionProDLL });
        }
        uxutil.localStorage.set(me.config.localTotalName, JSON.stringify(local));
    };
    //删除小组保存到local中
    app.delLocal = function (sectionID) {
        var me = this;
        if (!sectionID) return;
        //删除local中记录的小组
        var local = uxutil.localStorage.get(me.config.localTotalName, true);
        if (local) {
            if (local[me.config.userID]) {//存在当前等录人记录
                if (local[me.config.userID][me.config.localSectionName] && local[me.config.userID][me.config.localSectionName].length > 0) {//local中存储打开的小组
                    $.each(local[me.config.userID][me.config.localSectionName], function (i, item) {
                        if (item["Id"] == sectionID) {
                            local[me.config.userID][me.config.localSectionName].splice(i, 1);
                            return false;
                        }
                    });
                }
            }
            uxutil.localStorage.set(me.config.localTotalName, JSON.stringify(local));//保存local
        }
    };

    //初始化消息集成器
    app.initMsgintegrator = function () {
        var me = this;
        //启用消息集成器
        msgintegrator.init({
            //实例化正常后触发方法,instance:消息集成器实例
            done: function (instance) {
                layer.msg("消息集成器已准备就绪", { icon: 6, anim: 0, offset:'15px' });
                window.console && console.log && console.log("消息集成器已准备就绪");
                //注册消息推送业务
                app.onRegisterMsg();
            },
            //实例化异常时触发方法,info:{code:'错误信息编码',msg:'错误信息概要',desc:'错误信息详细'}
            error: function (info) {
                layer.msg(info.msg || "实例化异常", { icon: 5, anim: 0, offset: '15px' });
                window.console && console.error && console.error(info.desc, info.msg);
            },
            //通信连接后触发,instance:消息集成器实例
            connected: {
                name: 'text_' + new Date().getTime(),
                fun: function (instance) {
                    layer.msg("通信连接成功", { icon: 6, anim: 0, offset: '15px' });
                    window.console && console.log && console.log("通信连接成功");
                }
            },
            //通信连接断开后触发,返回尝试重连次数
            disconnected: {
                name: 'text_' + new Date().getTime(),
                fun: function (reconnectCount) {
                    layer.msg("通信中断", { icon: 0, anim: 0, offset: '15px' });
                    window.console && console.error && console.error("通信中断");
                }
            }
        });
    };
    //注册消息推送业务
    app.onRegisterMsg = function (callback) {
        var me = this,
            iTimer = null,
            icons = { "0": { icon: '√', color: 'green' }, "-1": { icon: '×', color: 'red' }, "-2": { icon: '!!', color: 'red' } };
        //成功标志 SuccessFlag：0:成功；-1:失败；-2:放弃处理"
        //注册消息业务
        msgintegrator.register({
            "name": 'test_msg_' + new Date().getTime(),
            fun: function (FormUserEmpId, FormUserEmpName, MessageInfo, MessageType) {
                switch (String(MessageType)) {
                    //通讯结果消息  对应枚举
                    case "TXJGMSG":
                        //显示消息
                        var info = $.parseJSON(MessageInfo) || null;
                        //var info = {
                        //    "ResultMsgName": "通讯",
                        //    "EquipFormID": "5215411710489224613",
                        //    "TestFormID": "5626461980855433443",
                        //    "EquipID": 11200000039,
                        //    "EquipName": "AU580012",
                        //    "BarCode": "123456789012",
                        //    "GSampleNo": "9",
                        //    "GTestDate": "2021-08-23",
                        //    "DataProcessType": "检验结果导入",
                        //    "SuccessFlag": 0,
                        //    "SuccessHint": "成功",
                        //    "ErrorInfo": "",
                        //    "OperTime": "2021-08-24 18:56:24",
                        //    "SectionID": "10000000039",
                        //    "SectionName": "生化AU580012"
                        //};
                        if (!info) return;
                        $("#EquipResultMsg").slideToggle("500");
                        var msg = info.OperTime + " " + info.ResultMsgName + "<span style='color:" + icons[info.SuccessFlag]["color"] + ";'>" + icons[info.SuccessFlag]["icon"] + "</span> 仪器：<span style='color:" + icons[info.SuccessFlag]["color"] + ";'>" + info.EquipName + "</span> 样本：<span style='color:" + icons[info.SuccessFlag]["color"] + ";'>" + info.BarCode + "(" + info.GSampleNo + ")</span> " + info.DataProcessType + " " + info.SuccessHint + (info.ErrorInfo ? ("：" + info.ErrorInfo) : "");
                        $("#EquipResultMsg").attr('title',msg);
                        $("#EquipResultMsg").html(msg);
                        $("#EquipResultMsg").slideToggle("500");
                        //上传数据处理
                        me.onUploadDataHandle(info);
                        //执行刷新处理
                        var CurrentSection = $("#Tab>.layui-tab-title").find('.layui-this').attr("lay-id");
                        if (!iTimer && me.config.loadSection[CurrentSection]) {//没有开启定时器 并且上传数据中存在当前小组数据 则开启定时器 执行刷新
                            iTimer = setTimeout(function () {
                                //当前小组刷新处理
                                me.onEquipResultMsgRefresh(CurrentSection);
                                iTimer = null;
                            }, me.config.loadTime);
                        }
                        break;
                    //危急值消息  对应枚举
                    case "WJZMSG":
                        //不存在锁屏时 弹出
                        if (me.config.CVLockLayerIndex == null) me.onCVTip();
                        break;
                    default:
                        break;
                }
                //layer.msg(new Date() + ": MessageInfo=" + MessageInfo + ";MessageType=" + MessageType + ";FormUserEmpId=" + FormUserEmpId + ";FormUserEmpName=" + FormUserEmpName, { icon: 6, anim: 0 });
                //window.console && console.log && console.log(new Date() + ": MessageInfo=" + MessageInfo + ";MessageType=" + MessageType + ";FormUserEmpId=" + FormUserEmpId + ";FormUserEmpName=" + FormUserEmpName);
            }
        });
        callback && callback();
    };
    //上传数据处理
    app.onUploadDataHandle = function (MessageInfo) {
        var me = this,
            MessageInfo = MessageInfo || null,
            SectionID = (MessageInfo && MessageInfo["SectionID"]) || null,
            TestFormID = (MessageInfo && MessageInfo["TestFormID"]) || null;

        if (!MessageInfo || !SectionID) return;

        //没有打开了该小组
        if ($("#Tab>.layui-tab-title").find('li[lay-id="' + SectionID + '"]').length == 0) return;
        //存储上传数据
        if (!me.config.loadSection[SectionID]) me.config.loadSection[SectionID] = [];//不存在 则添加空数组
        if (TestFormID) me.config.loadSection[SectionID].push(TestFormID);//存在样本单ID 则添加到该小组上传数据中
    };
    //执行topToolBar中开放的仪器上传刷新方法
    app.onEquipResultMsgRefresh = function (CurrentSection) {
        var me = this,
            loadSection = me.config.loadSection,
            CurrentSection = CurrentSection || $("#Tab>.layui-tab-title").find('.layui-this').attr("lay-id");//当前小组
        //当前小组刷新处理
        if (loadSection[CurrentSection] && loadSection[CurrentSection].length > 0) {
            $("#Tab>.layui-tab-content").find('#Iframe' + CurrentSection)[0].contentWindow.layui.topToolBar.onEquipResultMsgRefreshHandle(loadSection[CurrentSection]);
            delete me.config.loadSection[CurrentSection];
        }
    };
    //执行index中开放的设置Dom元素高度方法
    app.onInterfaceSetDomHeight = function (CurrentSection) {
        var me = this,
            CurrentSection = CurrentSection || $("#Tab>.layui-tab-title").find('.layui-this').attr("lay-id"),//当前小组
            IframeId = CurrentSection && CurrentSection != 0 ? '#Iframe' + CurrentSection : "#IframeAllSection";

        if ($("#Tab>.layui-tab-content").find(IframeId).length > 0 && $("#Tab>.layui-tab-content").find(IframeId)[0].contentWindow.layui && $("#Tab>.layui-tab-content").find(IframeId)[0].contentWindow.layui.SampleIndex)
            $("#Tab>.layui-tab-content").find(IframeId)[0].contentWindow.layui.SampleIndex.setDomHeight();
    };
    /*
     * **  通讯消息处理前台刷新逻辑  ** *
    通讯导入数据
	    当前定位的小组
		    编辑检验单
			    当前没有操作
				    直接刷新检验单列表 定位到之前定位的检验单上+样本单信息+检验结果
			    当前存在操作 但导入的数据不是当前操作的数据
				    刷新检验单列表 定位到操作的检验单上 检验单信息保留 检验结果保留
			    当前存在操作 导入数据包含当前操作数据
				    刷新检验单列表 定位到操作的检验单上 检验单信息保留 检验结果刷新
		    新增检验单
			    刷新检验单列表 定位到新增状态 检验单信息保留
	    其他打开的小组
		    记录刷新状态 切换到该小组时刷新 刷新逻辑同当前定位的小组

	    刷新时间 --可设置为参数
		    收到本小组消息后 5秒刷新 定时器置0，还有消息则再过5秒刷新 直到没有收到消息
     
     * **
     */
    //危急值消息提示遮罩层
    app.onCVTip = function () {
        var me = this;
            //list = list || [];
        if (!me.config.isOpenCV) return;
        //if (list.length == 0) return;
        var html = [
            '<div id="UnlockBox">',
            '<blockquote class="layui-elem-quote">发现危机结果报警，请立即查阅!</blockquote>',
            '<div class="layui-form" style="padding:5px;">',
            '<div class="layui-form-item">',
            '<input type="text" id="userpassword" onFocus="this.type=\'password\'" lay-verify="required" placeholder="解锁密码" autocomplete="new-password" class="layui-input">',
            '</div>',
            '<div class="layui-form-item">',
            '<button type="button" id="Unlock" class="layui-btn layui-btn-sm layui-btn-fluid" lay-submit lay-filter="Unlock"><i class="layui-icon layui-icon-password"></i>解锁</button>',
            '</div>',
            '</div>',
            '</div>'
        ];
        me.config.CVLockLayerIndex = layer.open({
            type: 1,
            area: '400px',
            title:'<span style="color:red;font-weight:bold;">危急值警报</span>',
            closeBtn: 0, //不显示关闭按钮
            shadeClose: false, //开启遮罩关闭
            content: html.join(''),
            success: function () {
                $("#userpassword").focus();
            }
        });

        //监听解锁按钮
        form.on('submit(Unlock)', function (data) {
            var account = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME),
                password = $("#userpassword").val();
            //危急值解锁按钮处理
            me.Unlock(account, password);
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });
        //回车解锁
        $("#UnlockBox").keydown(function (event) {
            switch (event.keyCode) {
                case 13:
                    //判断焦点是否在该输入框
                    if (document.activeElement == document.getElementById("userpassword")) {
                        var account = uxutil.cookie.get(uxutil.cookie.map.ACCOUNTNAME),
                            password = $("#userpassword").val();
                        //危急值解锁按钮处理
                        me.Unlock(account, password);
                    }
            }
        });
    };
    //危急值解锁按钮处理
    app.Unlock = function (account, password) {
        var me = this,
            account = account || null,
            password = password || null,
            url = me.url.LOGIN_URL;//解锁验证服务地址
        if (!account || !password) return;
        var load = layer.load();
        //请求登入接口
        uxutil.server.ajax({
            url: url,
            data: {
                strUserAccount: account,
                strPassWord: password
            }
        }, function (data) {
            layer.close(load);
            if (data === true) {
                layer.close(me.config.CVLockLayerIndex);
                me.config.CVLockLayerIndex = null;
                //弹出危急值查看页面
                layer.open({
                    type: 2,
                    area: ['90%', '90%'],
                    fixed: false,
                    maxmin: false,
                    title: '危急值查看处理',
                    content: 'cv/index.html',
                    success: function (layero, index) { }
                });
            } else {
                layer.msg('解锁密码错误！', { icon: 5, anim: 0 });
            }
        });
    };

    //根据小组类型获得配置JSON
    app.getModuleConfigJsonBySectionType = function (SectionType) {
        var me = this,
            SectionType = SectionType || me.config.defaultSectionType,
            moduleJson = me.config.moduleJson || null,
            SECTIONTYPEJSON = null;

        if (!moduleJson) {
            uxbase.MSG.onWarn("请在功能模块配置该小组类型界面配置JSON!");
            return null;
        }

        $.each(moduleJson, function (i, item) {
            if (item["name"] == SectionType) {
                SECTIONTYPEJSON = item["list"];
                return false;
            }
        });
        return SECTIONTYPEJSON;
    };

    //初始化
    app.init();
});