/**
	@name：状态公共处理
	@author：zhangda
	@version 2021-05-14
 */
layui.extend({
}).define(['uxutil','form', 'table'], function (exports) {
	"use strict";

	var $ = layui.$,
        table = layui.table,
        form = layui.form,
		uxutil = layui.uxutil;

	var app = {};
    //过滤条件复选框属性列表
    app.STATUS_LIST = [
        { text: '验中', iconText: '检', name: 'testing', color: '#FFB800', where: 'listestform.MainStatusID=0', checked: true },
        { text: '验确认', iconText: '检', name: 'testConfirm', color: '#5FB878', where: 'listestform.MainStatusID=2', checked: true },
        { text: '核', iconText: '审', name: 'check', color: '#009688', where: 'listestform.MainStatusID=3', checked: true },
        { text: '审', iconText: '反', name: 'backCheck', color: '#1E9FFF', where: '(listestform.MainStatusID=0 and listestform.CheckTime<>null)' },
        { text: '', iconText: '废', name: 'void', color: '#c2c2c2', where: 'listestform.MainStatusID=-2' },
        { text: '检', iconText: '复', name: 'recheck', color: '#2F4056', where: 'listestform.RedoStatus=1 or listestform.RedoStatus=2' }
    ];
    //状态样色
    app.ColorMap = {
        //主要提示
        '基本信息存在': { type: 'main', text: '基本信息存在', iconText: "<i class='layui-icon layui-icon-ok-circle' style='font-size:14px;color:blue;'></i>", color: '#fff', background: 'transparent', border: 'none', padding: '0' },
        '项目检验完成': { type: 'main', text: '项目检验完成', iconText: "<i class='layui-icon layui-icon-vercode' style='font-size:14px;'></i>", color: 'green', background: 'transparent', border: 'none', padding: '0' },
        '智能审核通过': { type: 'main', text: '智能审核通过', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/pass.png' />", color: 'green', background: 'transparent', border: 'none', padding: '0' },
        '智能审核失败': { type: 'main', text: '智能审核失败', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/fail.png' />", color: 'red', background: 'transparent', border: 'none', padding: '0' },
        //其他提示
        '急诊': { type: 'other', text: '急诊', iconText: '急', color: 'red', background: 'transparent', border: '1px solid red', padding: '0' },
        '建议复检': { type: 'other', text: '建议复检', iconText: "<i class='layui-icon layui-icon-refresh-1' style='font-size:14px;color:#E69139;'></i>", color: '#E69139', background: 'transparent', border: 'none', padding: '0' },
        '特殊样本': { type: 'other', text: '特殊样本', iconText: '特', color: '#DD6909', background: 'transparent', border: '1px solid #DD6909', padding: '0' },
        '打印': { type: 'other', text: '打印', iconText: "<i class='layui-icon layui-icon-print'></i>", color: '#000', background: 'transparent', border: 'none', padding: '0' },
        '警示': { type: 'other', text: '警示', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-1.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '警告': { type: 'other', text: '警告', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/1-2.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '严重警告': { type: 'other', text: '严重警告', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-4.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '危急': { type: 'other', text: '危急', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-6.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '发布成功': { type: 'other', text: '发布成功', iconText: "<i class='layui-icon'>&#xe609;</i>", color: 'green', background: 'transparent', border: 'none', padding: '0' },
        '发布失败': { type: 'other', text: '发布失败', iconText: "<i class='iconfont'>&#xe6de;</i>", color: 'red', background: 'transparent', border: 'none', padding: '0' },
        //'错误': { type: 'other', text: '错误', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-5.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },

        //"仪器通过": { type: 'other', text: '√仪器状态通过', iconText: '', color: 'green', background: 'green', border: 'none', padding: '0 2px' },
        //"仪器警告": { type: 'other', text: '仪器状态警告', iconText: '', color: 'orange', background: 'orange', border: 'none', padding: '0 2px' },
        //"仪器异常": { type: 'other', text: '×仪器状态异常', iconText: '', color: 'red', background: 'red', border: 'none', padding: '0 2px' },
        //"阳性保卡": { type: 'other', text: '阳性保卡', iconText: '', color: '#F08080', background: '#F08080', border: 'none', padding: '0 2px' }
    };
    //主状态样式模板
    app.MainStatusTemplet = '<span style="margin-right:1px;border:{border};background-color:{backgroundColor};position: relative;top: -1px;"><a style="color:{color};">{text}</a></span>';
    //颜色条模板
    app.ColorLineTemplet = '<span style="padding:0 1px;margin-right:1px;border:1px solid {color};background:{color};"></span>';
    //悬浮内容样式
    app.TipsTemplet = '<span style=\'margin:1px 1px 0 0;border:{border};color:{color};background:{background};\'>{title}</span><span style=\'color:#000;\'>{text}</span>';

    //鼠标悬浮显示
    app.onShowTips = function (record) {
        var me = this,
            html = [];

        html.push(
            '<div style=\'text-align:center;color:#000;\'><b>状态信息</b></div>' +
            '<div>' +
            '<a>检验日期：' + uxutil.date.toString(record['LisTestForm_GTestDate'], true) + '</a>' +
            '<a style=\'margin-left:10px;\'>' + record['LisTestForm_CName'] +
            '(' + record['LisTestForm_LisPatient_GenderName'] +
            ')</a>' +
            '</div>'
        );

        //主状态->检0、初2、终3、反(0 and CheckTime<>null)、废-2
        var MainStatusID = record['LisTestForm_MainStatusID'],
            CheckTime = record['LisTestForm_CheckTime'];

        if (MainStatusID == '0') {
            if (!CheckTime) {
                html.push(me.TipsTemplet.replace(/{color}/g, '#ffffff').replace(/{border}/g, '1px solid '+me.STATUS_LIST[0].color).replace(/{background}/g, me.STATUS_LIST[0].color).replace(/{title}/g, me.STATUS_LIST[0].iconText).replace(/{text}/g, (me.STATUS_LIST[0].iconText + me.STATUS_LIST[0].text)));
            } else {
                html.push(me.TipsTemplet.replace(/{color}/g, '#ffffff').replace(/{border}/g, '1px solid ' + me.STATUS_LIST[4].color).replace(/{background}/g, me.STATUS_LIST[4].color).replace(/{title}/g, me.STATUS_LIST[4].iconText).replace(/{text}/g, (me.STATUS_LIST[4].iconText + me.STATUS_LIST[4].text)));
            }
        } else if (MainStatusID == '2') {
            html.push(me.TipsTemplet.replace(/{color}/g, '#ffffff').replace(/{border}/g, '1px solid ' + me.STATUS_LIST[1].color).replace(/{background}/g, me.STATUS_LIST[1].color).replace(/{title}/g, me.STATUS_LIST[1].iconText).replace(/{text}/g, (me.STATUS_LIST[1].iconText + me.STATUS_LIST[1].text)));
        } else if (MainStatusID == '3') {
            html.push(me.TipsTemplet.replace(/{color}/g, '#ffffff').replace(/{border}/g, '1px solid ' + me.STATUS_LIST[2].color).replace(/{background}/g, me.STATUS_LIST[2].color).replace(/{title}/g, me.STATUS_LIST[2].iconText).replace(/{text}/g, (me.STATUS_LIST[2].iconText + me.STATUS_LIST[2].text)));
        } else if (MainStatusID == '-2') {
            html.push(me.TipsTemplet.replace(/{color}/g, '#ffffff').replace(/{border}/g, '1px solid ' + me.STATUS_LIST[4].color).replace(/{background}/g, me.STATUS_LIST[4].color).replace(/{title}/g, me.STATUS_LIST[4].iconText).replace(/{text}/g, (me.STATUS_LIST[4].iconText + me.STATUS_LIST[4].text)));
        }

        //基本信息存在->FormInfoStatus,0:未完成;1:已完成;
        var FormInfoStatus = record['LisTestForm_FormInfoStatus'];
        if (FormInfoStatus == '1') {
            html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.基本信息存在.color).replace(/{border}/g, me.ColorMap.基本信息存在.border).replace(/{background}/g, me.ColorMap.基本信息存在.background).replace(/{title}/g, me.ColorMap.基本信息存在.iconText).replace(/{text}/g, me.ColorMap.基本信息存在.text));
        }

        //检验完成->TestAllStatus,0:未开始;1:已开始;2:检验完成:绿色;(项目检验完成)
        var TestAllStatus = record['LisTestForm_TestAllStatus'];
        if (TestAllStatus == '1') {
            html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.项目检验完成.color).replace(/{border}/g, me.ColorMap.项目检验完成.border).replace(/{background}/g, me.ColorMap.项目检验完成.background).replace(/{title}/g, me.ColorMap.项目检验完成.iconText).replace(/{text}/g, me.ColorMap.项目检验完成.text));
        }

        //复检->RedoStatus,0/1
        var RedoStatus = record['LisTestForm_RedoStatus'];
        if (RedoStatus == '1') {
            html.push(me.TipsTemplet.replace(/{color}/g, '#ffffff').replace(/{border}/g, '1px solid ' + me.STATUS_LIST[5].color).replace(/{background}/g, me.STATUS_LIST[5].color).replace(/{title}/g, me.STATUS_LIST[5].iconText).replace(/{text}/g, (me.STATUS_LIST[5].iconText + me.STATUS_LIST[5].text)));
        } else if (RedoStatus == '2') {//建议复检
            html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.建议复检.color).replace(/{border}/g, me.ColorMap.建议复检.border).replace(/{background}/g, me.ColorMap.建议复检.background).replace(/{title}/g, me.ColorMap.建议复检.iconText).replace(/{text}/g, me.ColorMap.建议复检.text));
        }

        //样本号背景色-急诊->UrgentState!=''
        var UrgentState = record['LisTestForm_UrgentState'];
        if (UrgentState == '1') {
            html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.急诊.color).replace(/{border}/g, me.ColorMap.急诊.border).replace(/{background}/g, me.ColorMap.急诊.background).replace(/{title}/g, me.ColorMap.急诊.iconText).replace(/{text}/g, me.ColorMap.急诊.text));
        }

        //姓名背景色-标注样本（特殊样本）->SampleSpecialDesc!=''
        var SampleSpecialDesc = record['LisTestForm_SampleSpecialDesc'];
        if (SampleSpecialDesc) {
            html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.特殊样本.color).replace(/{border}/g, me.ColorMap.特殊样本.border).replace(/{background}/g, me.ColorMap.特殊样本.background).replace(/{title}/g, me.ColorMap.特殊样本.iconText).replace(/{text}/g, me.ColorMap.特殊样本.text));
        }

        ////仪器状态->ESendStatus,0:无;1:通过 绿色;2:警告 黄色;3:异常 红色;
        //var ESendStatus = record['LisTestForm_ESendStatus'] + '';
        //if (ESendStatus == '1') {
        //    html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.仪器通过.color).replace(/{border}/g, me.ColorMap.仪器通过.border).replace(/{background}/g, me.ColorMap.仪器通过.background).replace(/{title}/g, me.ColorMap.仪器通过.iconText).replace(/{text}/g, me.ColorMap.仪器通过.text));
        //} else if (ESendStatus == '2') {
        //    html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.仪器警告.color).replace(/{border}/g, me.ColorMap.仪器警告.border).replace(/{background}/g, me.ColorMap.仪器警告.background).replace(/{title}/g, me.ColorMap.仪器警告.iconText).replace(/{text}/g, me.ColorMap.仪器警告.text));
        //} else if (ESendStatus == '3') {
        //    html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.仪器异常.color).replace(/{border}/g, me.ColorMap.仪器异常.border).replace(/{background}/g, me.ColorMap.仪器异常.background).replace(/{title}/g, me.ColorMap.仪器异常.iconText).replace(/{text}/g, me.ColorMap.仪器异常.text));
        //}

        //警示级别
        var AlarmLevel = record['LisTestForm_AlarmLevel'];
        switch (String(AlarmLevel)) {
            case "1":
                html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.警示.color).replace(/{border}/g, me.ColorMap.警示.border).replace(/{background}/g, me.ColorMap.警示.background).replace(/{title}/g, me.ColorMap.警示.iconText).replace(/{text}/g, me.ColorMap.警示.text));
                break;
            case "2":
                html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.警告.color).replace(/{border}/g, me.ColorMap.警告.border).replace(/{background}/g, me.ColorMap.警告.background).replace(/{title}/g, me.ColorMap.警告.iconText).replace(/{text}/g, me.ColorMap.警告.text));
                break;
            case "3":
                html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.严重警告.color).replace(/{border}/g, me.ColorMap.严重警告.border).replace(/{background}/g, me.ColorMap.严重警告.background).replace(/{title}/g, me.ColorMap.严重警告.iconText).replace(/{text}/g, me.ColorMap.严重警告.text));
                break;
            case "4":
                html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.危急.color).replace(/{border}/g, me.ColorMap.危急.border).replace(/{background}/g, me.ColorMap.危急.background).replace(/{title}/g, me.ColorMap.危急.iconText).replace(/{text}/g, me.ColorMap.危急.text));
                break;
            default:
                break;
        }
        ////阳性保卡->ReportStatusID,倒数第1位=1
        //if (ReportStatusID && ReportStatusID.length >= 1 && ReportStatusID.charAt(ReportStatusID.length - 1) == '1') {
        //    html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.阳性保卡.color).replace(/{border}/g, me.ColorMap.阳性保卡.border).replace(/{background}/g, me.ColorMap.阳性保卡.background).replace(/{title}/g, me.ColorMap.阳性保卡.iconText).replace(/{text}/g, me.ColorMap.阳性保卡.text));
        //}

        //迁移标识
        var MigrationFlag = record['LisTestForm_MigrationFlag'] || "0";
        switch (String(MigrationFlag)) {
            case "0"://未发布
                break;
            case "1"://发布成功
                html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.发布成功.color).replace(/{border}/g, me.ColorMap.发布成功.border).replace(/{background}/g, me.ColorMap.发布成功.background).replace(/{title}/g, me.ColorMap.发布成功.iconText).replace(/{text}/g, me.ColorMap.发布成功.text));
                break;
            default://发布失败
                html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.发布失败.color).replace(/{border}/g, me.ColorMap.发布失败.border).replace(/{background}/g, me.ColorMap.发布失败.background).replace(/{title}/g, me.ColorMap.发布失败.iconText).replace(/{text}/g, me.ColorMap.发布失败.text));
                break;
        }

        //打印->PrintCount>0
        var PrintCount = record['LisTestForm_PrintCount'];
        if (PrintCount && PrintCount > 0) {
            html.push('<div style=\'color:#000;\'>已打印' + PrintCount + '次</div>');
        }

        //日期列背景色-智能审核->ZFSysCheckStatus,-1:智能审核不通过;0:无;1:智能审核通过; 
        var ZFSysCheckStatus = record['LisTestForm_ZFSysCheckStatus'];
        if (ZFSysCheckStatus == '1') {
            html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.智能审核通过.color).replace(/{border}/g, me.ColorMap.智能审核通过.border).replace(/{background}/g, me.ColorMap.智能审核通过.background).replace(/{title}/g, me.ColorMap.智能审核通过.iconText).replace(/{text}/g, me.ColorMap.智能审核通过.text));
        } else if (ZFSysCheckStatus == '-1') {
            html.push(me.TipsTemplet.replace(/{color}/g, me.ColorMap.智能审核失败.color).replace(/{border}/g, me.ColorMap.智能审核失败.border).replace(/{background}/g, me.ColorMap.智能审核失败.background).replace(/{title}/g, me.ColorMap.智能审核失败.iconText).replace(/{text}/g, me.ColorMap.智能审核失败.text));
            //失败原因
            var LisTestForm_ZFSysCheckInfo = record['LisTestForm_ZFSysCheckInfo'];
            if (LisTestForm_ZFSysCheckInfo.indexOf('[') != -1) {
                var ZFSysCheckInfo = LisTestForm_ZFSysCheckInfo ? $.parseJSON(LisTestForm_ZFSysCheckInfo) : [];
                $.each(ZFSysCheckInfo, function (i, item) {
                    html.push('<span style=\'padding-left:5px;background:#fff;color:' + me.ColorMap.智能审核失败.color + '\'>' + item.replace(RegExp('"', 'g'), '') + '</span>');
                });
            } else {//为了处理之前的数据
                html.push('<span style=\'padding-left:5px;background:#fff;color:' + me.ColorMap.智能审核失败.color + '\'>' + LisTestForm_ZFSysCheckInfo.replace(RegExp('"', 'g'), '') + '</span>');
            }
        }

        return html.join('</BR>');
    };
    //状态列处理
    app.onStatusRenderer = function (record, isD) {
        var me = this,
            isD = isD || isD,
            MainStatusID = isD ? record['LisTestForm_DMainStatusID'] : record['LisTestForm_MainStatusID'],//主状态->检0、初2、终3、反(0 and CheckTime<>null)、废-2
            CheckTime = isD ? record['LisTestForm_DCheckTime'] : record['LisTestForm_CheckTime'],//审核时间
            html = [];
        //主状态判断显示
        if (MainStatusID == '0') {
            if (!CheckTime) {
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.STATUS_LIST[0].border).replace(/{backgroundColor}/g, me.STATUS_LIST[0].color).replace(/{color}/g, '#fff').replace(/{text}/g, me.STATUS_LIST[0].iconText));
            } else {
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.STATUS_LIST[3].border).replace(/{backgroundColor}/g, me.STATUS_LIST[3].color).replace(/{color}/g, '#fff').replace(/{text}/g, me.STATUS_LIST[3].iconText));
            }
        } else if (MainStatusID == '2') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.STATUS_LIST[1].border).replace(/{backgroundColor}/g, me.STATUS_LIST[1].color).replace(/{color}/g, '#fff').replace(/{text}/g, me.STATUS_LIST[1].iconText));
        } else if (MainStatusID == '3') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.STATUS_LIST[2].border).replace(/{backgroundColor}/g, me.STATUS_LIST[2].color).replace(/{color}/g, '#fff').replace(/{text}/g, me.STATUS_LIST[2].iconText));
        } else if (MainStatusID == '-2') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.STATUS_LIST[4].border).replace(/{backgroundColor}/g, me.STATUS_LIST[4].color).replace(/{color}/g, '#fff').replace(/{text}/g, me.STATUS_LIST[4].iconText));
        }

        //基本信息存在->FormInfoStatus,0:未完成;1:已完成;
        var FormInfoStatus = isD ? record['LisTestForm_DFormInfoStatus'] : record['LisTestForm_FormInfoStatus'];
        if (FormInfoStatus == '1') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.基本信息存在.border).replace(/{backgroundColor}/g, me.ColorMap.基本信息存在.background).replace(/{color}/g, me.ColorMap.基本信息存在.color).replace(/{text}/g, me.ColorMap.基本信息存在.iconText));
        }

        //检验完成->TestAllStatus,0:未开始;1:检验完成:绿色;（项目检验完成）
        var TestAllStatus = isD ? record['LisTestForm_DTestAllStatus'] : record['LisTestForm_TestAllStatus'];
        if (TestAllStatus == '1') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.项目检验完成.border).replace(/{backgroundColor}/g, me.ColorMap.项目检验完成.background).replace(/{color}/g, me.ColorMap.项目检验完成.color).replace(/{text}/g, me.ColorMap.项目检验完成.iconText));
        }

        //复检->RedoStatus,0/1
        var RedoStatus = isD ? record['LisTestForm_DRedoStatus'] : record['LisTestForm_RedoStatus'];
        if (RedoStatus == '1') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.STATUS_LIST[5].border).replace(/{backgroundColor}/g, me.STATUS_LIST[5].color).replace(/{color}/g, '#fff').replace(/{text}/g, me.STATUS_LIST[5].iconText));
        } else if (RedoStatus == '2') {//建议复检
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.建议复检.border).replace(/{backgroundColor}/g, me.ColorMap.建议复检.background).replace(/{color}/g, me.ColorMap.建议复检.color).replace(/{text}/g, me.ColorMap.建议复检.iconText));
        }

        //样本号背景色-急诊->UrgentState!=''
        var UrgentState = isD ? record['LisTestForm_DUrgentState'] : record['LisTestForm_UrgentState'];
        if (UrgentState == '1') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.急诊.border).replace(/{backgroundColor}/g, me.ColorMap.急诊.background).replace(/{color}/g, me.ColorMap.急诊.color).replace(/{text}/g, me.ColorMap.急诊.iconText));
        }

        //姓名背景色-标注样本（特殊样本）->SampleSpecialDesc!=''
        var SampleSpecialDesc = isD ? record['LisTestForm_DSampleSpecialDesc'] : record['LisTestForm_SampleSpecialDesc'];
        if (SampleSpecialDesc) {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.特殊样本.border).replace(/{backgroundColor}/g, me.ColorMap.特殊样本.background).replace(/{color}/g, me.ColorMap.特殊样本.color).replace(/{text}/g, me.ColorMap.特殊样本.iconText));
        }

        //警示级别
        var AlarmLevel = isD ? record['LisTestForm_DAlarmLevel'] : record['LisTestForm_AlarmLevel'];
        switch (String(AlarmLevel)) {
            case "1":
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.警示.border).replace(/{backgroundColor}/g, me.ColorMap.警示.background).replace(/{color}/g, me.ColorMap.警示.color).replace(/{text}/g, me.ColorMap.警示.iconText));
                break;
            case "2":
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.警告.border).replace(/{backgroundColor}/g, me.ColorMap.警告.background).replace(/{color}/g, me.ColorMap.警告.color).replace(/{text}/g, me.ColorMap.警告.iconText));
                break;
            case "3":
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.严重警告.border).replace(/{backgroundColor}/g, me.ColorMap.严重警告.background).replace(/{color}/g, me.ColorMap.严重警告.color).replace(/{text}/g, me.ColorMap.严重警告.iconText));
                break;
            case "4":
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.危急.border).replace(/{backgroundColor}/g, me.ColorMap.危急.background).replace(/{color}/g, me.ColorMap.危急.color).replace(/{text}/g, me.ColorMap.危急.iconText));
                break;
            default:
                break;
        }

        //迁移标识
        var MigrationFlag = (isD ? record['LisTestForm_DMigrationFlag'] : record['LisTestForm_MigrationFlag']) || "0";
        switch (String(MigrationFlag)) {
            case "0"://未发布
                break;
            case "1"://发布成功
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.发布成功.border).replace(/{backgroundColor}/g, me.ColorMap.发布成功.background).replace(/{color}/g, me.ColorMap.发布成功.color).replace(/{text}/g, me.ColorMap.发布成功.iconText));
                break;
            default://发布失败
                html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.发布失败.border).replace(/{backgroundColor}/g, me.ColorMap.发布失败.background).replace(/{color}/g, me.ColorMap.发布失败.color).replace(/{text}/g, me.ColorMap.发布失败.iconText));
                break;
        }

        //智能审核->ZFSysCheckStatus,1:通过;-1:未通过
        var ZFSysCheckStatus = isD ? record['LisTestForm_DZFSysCheckStatus'] : record['LisTestForm_ZFSysCheckStatus'];
        if (ZFSysCheckStatus == '1') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.智能审核通过.border).replace(/{backgroundColor}/g, me.ColorMap.智能审核通过.background).replace(/{color}/g, me.ColorMap.智能审核通过.color).replace(/{text}/g, me.ColorMap.智能审核通过.iconText));
        } else if (ZFSysCheckStatus == '-1') {
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.智能审核失败.border).replace(/{backgroundColor}/g, me.ColorMap.智能审核失败.background).replace(/{color}/g, me.ColorMap.智能审核失败.color).replace(/{text}/g, me.ColorMap.智能审核失败.iconText));
        }

        ////仪器状态->ESendStatus,0:无;1:通过 绿色;2:警告 黄色;3:异常 红色;
        //var ESendStatus = isD ? record['LisTestForm_DESendStatus'] + '' : record['LisTestForm_ESendStatus'] + '';
        //if (ESendStatus == '1') {
        //    html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.仪器通过.color));
        //} else if (ESendStatus == '2') {
        //    html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.仪器警告.color));
        //} else if (ESendStatus == '3') {
        //    html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.仪器异常.color));
        //}

        ////阳性保卡->ReportStatusID,倒数第1位=1
        //var ReportStatusID = isD ? record['LisTestForm_DReportStatusID'] + '' : record['LisTestForm_ReportStatusID'] + '';
        //if (ReportStatusID && ReportStatusID.length >= 1 && ReportStatusID.charAt(ReportStatusID.length - 1) == '1') {
        //    html.push(me.ColorLineTemplet.replace(/{color}/g, me.ColorMap.阳性保卡.color));
        //}

        //打印->PrintCount>0
        var PrintCount = isD ? record['LisTestForm_DPrintCount'] : record['LisTestForm_PrintCount'];
        if (PrintCount && PrintCount > 0) {
            //html.push(me.PrintTemplet);
            html.push(me.MainStatusTemplet.replace(/{border}/g, me.ColorMap.打印.border).replace(/{backgroundColor}/g, me.ColorMap.打印.background).replace(/{color}/g, me.ColorMap.打印.color).replace(/{text}/g, me.ColorMap.打印.iconText));
        }

        return '<div class="tips" data-title="' + me.onShowTips(record) + '">' + html.join('') + '</div>';
    };

	//暴露接口
    exports('basicStatus', app);
});