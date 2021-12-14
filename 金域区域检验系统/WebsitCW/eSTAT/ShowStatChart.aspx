<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowStatChart.aspx.cs" Inherits="OA.YHY.ShowStatChart" %>

<%@ Register Assembly="dotnetCHARTING" Namespace="dotnetCHARTING" TagPrefix="dotnetCHARTING" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业信息统计</title>
    <link href="../App_Themes/zh-cn/DataUserControl.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="1" cellpadding="1" width="700" border="0">
        <tr>
            <td>
                <asp:Label ID="lblStatChartDataTitle" runat="server" CssClass="LableTitle" Visible="False">统计图数据</asp:Label>
                <asp:LinkButton ID="btnExport" runat="server" CssClass="LinkButton" OnClick="btnExport_Click">      导出数据</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="dsResult" runat="server" CssClass="DataList" FooterStyle-CssClass="DataListFoot" ItemStyle-CssClass="DataListLeft" FooterStyle-ForeColor="#ff0000" ItemStyle-Wrap="False" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="DataListColumn" ShowFooter="False">
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblShowType" runat="server" CssClass="LableTitle">显示类型</asp:Label>
                <asp:LinkButton ID="btnCombo" runat="server" CssClass="LinkButton" OnClick="btnCombo_Click">直方图</asp:LinkButton>
                <asp:LinkButton ID="btnComboHorizontal" runat="server" CssClass="LinkButton" OnClick="btnComboHorizontal_Click">水平直方图</asp:LinkButton>
                <asp:LinkButton ID="btnPies" runat="server" CssClass="LinkButton" OnClick="btnPies_Click">饼图</asp:LinkButton>
                <asp:LinkButton ID="btnPiesSplit" runat="server" CssClass="LinkButton" onclick="btnPiesSplit_Click">爆破饼图</asp:LinkButton>
                <asp:LinkButton ID="btnRing" runat="server" CssClass="LinkButton" OnClick="btnRing_Click">环型图</asp:LinkButton>
                <asp:LinkButton ID="btnColumn" runat="server" CssClass="LinkButton" OnClick="btnColumn_Click">柱型图</asp:LinkButton>
                <asp:LinkButton ID="btnArea" runat="server" CssClass="LinkButton" OnClick="btnArea_Click">面积图</asp:LinkButton>
                <asp:LinkButton ID="btnSpline" runat="server" CssClass="LinkButton" OnClick="btnSpline_Click">折线图</asp:LinkButton>
                <asp:LinkButton ID="btnLine" runat="server" CssClass="LinkButton" OnClick="btnLine_Click">直线图</asp:LinkButton>
                <asp:LinkButton ID="btnConvertXY" runat="server" CssClass="LinkButton" OnClick="btnConvertXY_Click">    变换维度</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="imageStatChart" runat="server"></asp:Image>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="LabelInput" style="white-space: nowrap" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="LableTitle" style="white-space: nowrap" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <dotnetCHARTING:Chart ID="ChartSTAT" runat="server" Visible="False" Width="296px" Height="16px">
                    <DefaultTitleBox>
                        <HeaderLabel GlowColor="" Type="UseFont">
                        </HeaderLabel>
                        <HeaderBackground ShadingEffectMode="None"></HeaderBackground>
                        <Background ShadingEffectMode="None"></Background>
                        <Label GlowColor="" Type="UseFont">
                        </Label>
                    </DefaultTitleBox>
                    <SmartForecast Start=""></SmartForecast>
                    <Background ShadingEffectMode="None"></Background>
                    <DefaultLegendBox Padding="4" CornerBottomRight="Cut">
                        <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                        <DefaultEntry ShapeType="None">
                            <Background ShadingEffectMode="None"></Background>
                            <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                        </DefaultEntry>
                        <HeaderEntry ShapeType="None" Visible="False">
                            <Background ShadingEffectMode="None"></Background>
                            <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                        </HeaderEntry>
                        <HeaderLabel GlowColor="" Type="UseFont">
                        </HeaderLabel>
                        <HeaderBackground ShadingEffectMode="None"></HeaderBackground>
                        <Background ShadingEffectMode="None"></Background>
                    </DefaultLegendBox>
                    <ChartArea CornerTopLeft="Square" StartDateOfYear="">
                        <DefaultElement ShapeType="None">
                            <DefaultSubValue Name="">
                            </DefaultSubValue>
                            <SmartLabel GlowColor="" Type="UseFont">
                            </SmartLabel>
                            <LegendEntry ShapeType="None">
                                <Background ShadingEffectMode="None"></Background>
                                <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                            </LegendEntry>
                        </DefaultElement>
                        <Label GlowColor="" Type="UseFont" Font="Tahoma, 8pt">
                        </Label>
                        <YAxis GaugeNeedleType="One" GaugeLabelMode="Default" SmartScaleBreakLimit="2">
                            <ScaleBreakLine Color="Gray"></ScaleBreakLine>
                            <TimeScaleLabels MaximumRangeRows="4">
                            </TimeScaleLabels>
                            <MinorTimeIntervalAdvanced Start=""></MinorTimeIntervalAdvanced>
                            <ZeroTick>
                                <Line Length="3"></Line>
                                <Label GlowColor="" Type="UseFont">
                                </Label>
                            </ZeroTick>
                            <DefaultTick>
                                <Line Length="3"></Line>
                                <Label GlowColor="" Type="UseFont" Text="%Value">
                                </Label>
                            </DefaultTick>
                            <TimeIntervalAdvanced Start=""></TimeIntervalAdvanced>
                            <AlternateGridBackground ShadingEffectMode="None"></AlternateGridBackground>
                            <Label GlowColor="" Type="UseFont" Alignment="Center" LineAlignment="Center" Font="Arial, 9pt, style=Bold">
                            </Label>
                        </YAxis>
                        <XAxis GaugeNeedleType="One" GaugeLabelMode="Default" SmartScaleBreakLimit="2">
                            <ScaleBreakLine Color="Gray"></ScaleBreakLine>
                            <TimeScaleLabels MaximumRangeRows="4">
                            </TimeScaleLabels>
                            <MinorTimeIntervalAdvanced Start=""></MinorTimeIntervalAdvanced>
                            <ZeroTick>
                                <Line Length="3"></Line>
                                <Label GlowColor="" Type="UseFont">
                                </Label>
                            </ZeroTick>
                            <DefaultTick>
                                <Line Length="3"></Line>
                                <Label GlowColor="" Type="UseFont" Text="%Value">
                                </Label>
                            </DefaultTick>
                            <TimeIntervalAdvanced Start=""></TimeIntervalAdvanced>
                            <AlternateGridBackground ShadingEffectMode="None"></AlternateGridBackground>
                            <Label GlowColor="" Type="UseFont" Alignment="Center" LineAlignment="Center" Font="Arial, 9pt, style=Bold">
                            </Label>
                        </XAxis>
                        <Background ShadingEffectMode="None"></Background>
                        <TitleBox Position="Left">
                            <HeaderLabel GlowColor="" Type="UseFont">
                            </HeaderLabel>
                            <HeaderBackground ShadingEffectMode="None"></HeaderBackground>
                            <Background ShadingEffectMode="None"></Background>
                            <Label GlowColor="" Type="UseFont" Color="Black">
                            </Label>
                        </TitleBox>
                        <LegendBox Padding="4" Position="Top" CornerBottomRight="Cut">
                            <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                            <DefaultEntry ShapeType="None">
                                <Background ShadingEffectMode="None"></Background>
                                <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                            </DefaultEntry>
                            <HeaderEntry ShapeType="None" Name="Name" Value="Value" Visible="False" SortOrder="-1">
                                <Background ShadingEffectMode="None"></Background>
                                <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                            </HeaderEntry>
                            <HeaderLabel GlowColor="" Type="UseFont">
                            </HeaderLabel>
                            <HeaderBackground ShadingEffectMode="None"></HeaderBackground>
                            <Background ShadingEffectMode="None"></Background>
                        </LegendBox>
                    </ChartArea>
                    <DefaultElement ShapeType="None">
                        <DefaultSubValue Name="">
                        </DefaultSubValue>
                        <SmartLabel GlowColor="" Type="UseFont">
                        </SmartLabel>
                        <LegendEntry ShapeType="None">
                            <Background ShadingEffectMode="None"></Background>
                            <LabelStyle GlowColor="" Type="UseFont"></LabelStyle>
                        </LegendEntry>
                    </DefaultElement>
                    <NoDataLabel GlowColor="" Type="UseFont">
                    </NoDataLabel>
                    <TitleBox Position="Left">
                        <HeaderLabel GlowColor="" Type="UseFont">
                        </HeaderLabel>
                        <HeaderBackground ShadingEffectMode="None"></HeaderBackground>
                        <Background ShadingEffectMode="None"></Background>
                        <Label GlowColor="" Type="UseFont" Color="Black">
                        </Label>
                    </TitleBox>
                </dotnetCHARTING:Chart>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
