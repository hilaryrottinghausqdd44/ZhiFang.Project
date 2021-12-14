<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Functions.LicenseComm" Codebehind="LicenseComm.aspx.cs" %>
<HTML>
	<HEAD>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<style type="text/css">BODY { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	A { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	TABLE { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	DIV { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	SPAN { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	TD { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	TH { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	INPUT { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	SELECT { FONT: 9pt "宋体", Verdana, Arial, Helvetica, sans-serif }
	BODY { PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px }
	</style>
		<script language="JavaScript" src="dialog.js"></script>
		<script language="javascript">
			//window.returnValue = null;
			//window.close();
			function ok()
			{
				parent.window.returnValue = Form2.TextBoxLicenseNo.value;
				parent.window.close();
			}
		</script>
	</HEAD>
	<body oncontextmenu="return false" onselectstart="return false" ondragstart="return false"
		bgColor="menu">
		<form id="Form2" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<td>
						<fieldset>
							<legend>检验之星通讯注册号生成模块</legend>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>网卡号:</td>
									<td width="5"></td>
									<td width="216" colSpan="5"><asp:textbox id="TextBoxNetworkCardNo" runat="server" Width="341px"></asp:textbox></td>
									<td width="7"></td>
								</tr>
								<TR>
									<TD width="7"></TD>
									<TD></TD>
									<TD width="5"></TD>
									<TD width="216"></TD>
									<TD width="40"></TD>
									<TD></TD>
									<TD width="5"></TD>
									<TD></TD>
									<TD width="7"></TD>
								</TR>
								<TR>
									<TD width="7"></TD>
									<TD>授权码:</TD>
									<TD width="5"></TD>
									<TD width="216"><asp:textbox id="TextBoxLicenseNo" runat="server" Width="234px"></asp:textbox></TD>
									<TD width="40" colSpan="4"><asp:button id="ButtonCreate" runat="server" Text="生成授权号" onclick="ButtonCreate_Click"></asp:button></TD>
									<TD width="7"></TD>
								</TR>
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td height="5"></td>
				</tr>
				<tr>
					<td>
						<fieldset>
							<legend>授权有效规则</legend>
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>授权类型:</td>
									<td width="5"></td>
									<td width="111"><select id="drpLicenseType" style="WIDTH: 104px" runat="server">
											<option value="商业" selected>商业</option>
											<option value="临时">临时</option>
										</select>
									</td>
									<td width="40"></td>
									<td>程序模块:</td>
									<td width="5"></td>
									<td><SELECT id="drpModule" style="WIDTH: 112px" name="Select1" runat="server">
											<option value=“3001”>CA1500</option>
											<option value=“3002”>CA6000</option>
											<option value=“3003”>CA500</option>
											<option value=“3004”>CA7000</option>
											<option value=“3005”>XE2100</option>
											<option value=“3006”>CA50</option>
											<option value=“3007”>CA530</option>
											<option value=“3008”>Ca550</option>
											<option value=“3009”>XT1800</option>
											<option value=“3010”>FASCO-3010</option>
											<option value=“3011”>XT2000</option>
											<option value=“3051”>Elecsys 1010</option>
											<option value=“3052”>Elecsys 2010</option>
											<option value=“3053”>cobas MIRS PLUS</option>
											<option value=“3054”>Reflotron</option>
											<option value=“3055”>PCR</option>
											<option value=“3056”>I400</option>
											<option value=“3057”>I800</option>
											<option value=“3101”>7020</option>
											<option value=“3102”>7060</option>
											<option value=“3103”>7080</option>
											<option value=“3104”>7150</option>
											<option value=“3105”>7170</option>
											<option value=“3106”>7250</option>
											<option value=“3107”>7600</option>
											<option value=“3109”>7180</option>
											<option value=“3140”>Dirih500</option>
											<option value=“3150”>ADVIA Bayer 120</option>
											<option value=“3151”>CLINITEK 200+</option>
											<option value=“3152”>CLINITEK 100</option>
											<option value=“3153”>Acs180SE</option>
											<option value=“3154”>Medica easybloodcas</option>
											<option value=“3155”>Clinitek_status</option>
											<option value=“3201”>Dimension AR</option>
											<option value=“3202”>Dimension Rxl</option>
											<option value=“3203”>BN-100</option>
											<option value=“3204”>X-Pand</option>
											<option value=“3205”>BT_2000</option>
											<option value=“3206”>MicroScan Walkaway 40</option>
											<option value=“3207”>Opus Plus</option>
											<option value=“3208”>BN ProSpec</option>
											<option value=“3209”>BN2</option>
											<option value=“3251”>MEK-6318K</option>
											<option value=“3252”>MEK-5108</option>
											<option value=“3253”>MEK-6108</option>
											<option value=“3281”>TBA30FR</option>
											<option value=“3282”>TBA40FR</option>
											<option value=“3283”>TBA120</option>
											<option value=“3301”>"CX5”>CX4"</option>
											<option value=“3302”>ARRAY360</option>
											<option value=“3303”>SynchronELISE</option>
											<option value=“3304”>CX3</option>
											<option value=“3305”>Gen.S</option>
											<option value=“3306”>CX9</option>
											<option value=“3351”>648PC</option>
											<option value=“3371”>ESR-30</option>
											<option value=“3381”>DSI-905</option>
											<option value=“3382”>DSI-903</option>
											<option value=“3401”>MD2</option>
											<option value=“3402”>MAXM</option>
											<option value=“3403”>COULTER ACT3</option>
											<option value=“3404”>CL-7000</option>
											<option value=“3405”>CL-7200</option>
											<option value=“3406”>ACL200</option>
											<option value=“3407”>AUTOLAB18</option>
											<option value=“3408”>Kodak750</option>
											<option value=“3409”>Kodak250</option>
											<option value=“3410”>DS5</option>
											<option value=“3411”>96I</option>
											<option value=“3412”>COULTER HMX</option>
											<option value=“3413”>COULTER ACT5</option>
											<option value=“3501”>CELL-DYN1700</option>
											<option value=“3502”>CELL-DYN1200</option>
											<option value=“3503”>CELL-DYN3500</option>
											<option value=“3504”>CELL-DYN1800</option>
											<option value=“3505”>cd1600</option>
											<option value=“3602”>F820</option>
											<option value=“3603”>KX_21</option>
											<option value=“3701”>VIDAS</option>
											<option value=“3801”>JUNIOR</option>
											<option value=“3801”>JUNIOR2</option>
											<option value=“3802”>Nova</option>
											<option value=“3803”>URISCAN-PRO</option>
											<option value=“3804”>CLINITEK 500</option>
											<option value=“3805”>DIANA 5</option>
											<option value=“3806”>Triturus DG-53</option>
											<option value=“3807”>AU400</option>
											<option value=“3808”>DPC-Immulite</option>
											<option value=“3809”>AM-4290</option>
											<option value=“3810”>Monitor-J+</option>
											<option value=“3811”>Miditron_M</option>
											<option value=“3812”>ALisei</option>
											<option value=“3813”>STA</option>
											<option value=“3814”>STKS</option>
											<option value=“3815”>AU2700</option>
											<option value=“3816”>au640</option>
											<option value=“3817”>au5400</option>
											<option value=“3820”>K4500</option>
											<option value=“3901”>AVL 9181(9180)</option>
											<option value=“3902”>OMNI</option>
											<option value=“3903”>20</option>
											<option value=“3904”>ciba560</option>
											<option value=“3905”>AVL988-3</option>
											<option value=“3906”>248</option>
											<option value=“3911”>4270</option>
											<option value=“3912”>4280</option>
											<option value=“3920”>Access</option>
											<option value=“3931”>IL synthesis 20</option>
											<option value=“3932”>acl6000</option>
											<option value=“3933”>ACL9000</option>
											<option value=“3940”>Immage</option>
											<option value=“3950”>Keysys</option>
											<option value=“3960”>SCAN300</option>
											<option value=“3961”>YD-S3000</option>
											<option value=“3970”>UF100</option>
											<option value=“3971”>uf-50</option>
											<option value=“3981”>ABX-Micros60</option>
											<option value=“3982”>ABX-Micros61</option>
											<option value=“3990”>SF3000</option>
											<option value=“3992”>Nova16+</option>
											<option value=“3993”>nova  4+</option>
											<option value=“3994”>NOVA_Stat</option>
											<option value=“4001”>Sebia</option>
											<option value=“4002”>Coulter JT</option>
											<option value=“4003”>AMP</option>
											<option value=“4004”>CX7</option>
											<option value=“4005”>AXSYM</option>
											<option value=“4006”>VITEK 2</option>
											<option value=“4007”>R_500</option>
											<option value=“4008”>VITEK250</option>
											<option value=“4009”>747</option>
											<option value=“4011”>COMPACT</option>
											<option value=“4012”>LX20</option>
											<option value=“4014”>VITEK</option>
											<option value=“4015”>E-170</option>
											<option value=“4016”>埃尔默1235－320 LISA</option>
											<option value=“4016-2”>ARCUS1235荧光仪</option>
											<option value=“4017”>URISYS尿沉渣</option>
											<option value=“4018”>abl555血气</option>
											<option value=“4050”>伯乐550</option>
											<option value=“4200”>普利生N6C</option>
											<option value=“4201”>Uritest200</option>
											<option value=“4202”>kone optima 981475</option>
											<option value=“4203”>ADVIA 60</option>
											<option value=“4204”>LBY-N6A</option>
											<option value=“4205”>Modular7600</option>
											<option value=“4206”>Uritest300</option>
											<option value=“4207”>BC2000</option>
											<option value=“4208”>BT3000</option>
											<option value=“4209”>SwelabAC970</option>
											<option value=“4210”>DFM_A</option>
											<option value=“4211”>151</option>
											<option value=“4212”>AUTOSCAN4</option>
											<option value=“4213”>AVL-compact3</option>
											<option value=“4214”>903</option>
											<option value=“4215”>Phoenix100</option>
											<option value=“4216”>CLB128C</option>
											<option value=“4217”>Microtech 648</option>
											<option value=“4218”>MI-921D</option>
											<option value=“4219”>Uritest100</option>
											<option value=“4220”>excell18</option>
											<option value=“4221”>OMNI-C</option>
											<option value=“4222”>CELL-DYN3200</option>
											<option value=“4223”>Chrono-log5904d</option>
											<option value=“4224”>excell22</option>
											<option value=“4225”>血流变LGR80A</option>
											<option value=“4226”>RA1000</option>
											<option value=“4228”>spife3000</option>
											<option value=“4229”>BE-COMPACT211</option>
											<option value=“4230”>2001A</option>
											<option value=“4231”>cd171</option>
											<option value=“4232”>sonoclot血凝仪</option>
											<option value=“4233”>CODAII</option>
											<option value=“4234”>SEDI-15血沉仪</option>
											<option value=“4235”>bm200</option>
											<option value=“4270”>ABL700</option>
											<option value=“4330”>Se9000</option>
											<option value=“4331”>uriscan500</option>
											<option value=“4332”>URISYS2400</option>
											<option value=“4333”>liaison  量速</option>
											<option value=“4334”>ETI-MAX3000</option>
											<option value=“4335”>CELL-DYN3700</option>
											<option value=“4336”>bayer855血气分析</option>
											<option value=“4337”>Diasys</option>
											<option value=“4338”>FAME</option>
											<option value=“4339”>GC-911</option>
											<option value=“4340”>AT-2000</option>
											<option value=“4341”>Sa6000</option>
											<option value=“4342”>Merck</option>
											<option value=“4343”>Eci</option>
											<option value=“4344”>AutoBio-2.8</option>
											<option value=“4345”>Cl8000</option>
											<option value=“4346”>Act2Diff</option>
											<option value=“4347”>Dml2000</option>
											<option value=“4348”>Pro2100</option>
											<option value=“4349”>C8000</option>
											<option value=“4350”>戴安娜半自动</option>
											<option value=“4351”>By100</option>
											<option value=“4352”>ATB</option>
											<option value=“4353”>I2000SR</option>
											<option value=“4354”>BioMic</option>
											<option value=“4355”>TDxFLx</option>
											<option value=“4356”>TRT4</option>
											<option value=“4357”>DFM-96</option>
											<option value=“4358”>IMx</option>
											<option value=“4359”>bio-rad</option>
											<option value=“4360”>Vital Microlab300</option>
											<option value=“4361”>Clintek50</option>
											<option value=“4362”>Roche OPTI CCA</option>
											<option value=“4363”>POCH－100I</option>
											<option value=“4364”>Medica EasyLyte</option>
											<option value=“4365”>CA-570</option>
											<option value=“4366”>Bayer1650</option>
											<option value=“4367”>Dirih300</option>
											<option value=“4368”>Dirih100</option>
											<option value=“4369”>R_BC3000</option>
											<option value=“4370”>Bt2000</option>
											<option value=“4371”>Imx</option>
											<option value=“4372”>CENTUAR</option>
											<option value=“4373”>Anthos 2010</option>
											<option value=“4374”>CIBA238</option>
											<option value=“4375”>SENSITITRO</option>
											<option value=“4376”>Ci8200</option>
											<option value=“4377”>Ms720</option>
											<option value=“4378”>ZC-C13</option>
											<option value=“4379”>PR-521</option>
											<option value=“4380”>S8</option>
											<option value=“4381”>AC-900+</option>
											<option value=“4382”>BC3000</option>
											<option value=“4383”>MA-4280</option>
											<option value=“4384”>MS2030</option>
											<option value=“4385”>PA200</option>
											<option value=“4386”>ELX800</option>
											<option value=“4387”>Biocrll 2010</option>
											<option value=“4388”>SN697</option>
											<option value=“4389”>US2020</option>
											<option value=“4390”>骨髓细胞分析</option>
											<option value=“4391”>SYM-BIO</option>
											<option value=“4392”>Dt60</option>
											<option value=“4393”>MDK－B100</option>
											<option value=“4394”>BIO-RAD Model 680</option>
											<option value=“4395”>GC-1200</option>
											<option value=“4396”>Mpc-1</option>
											<option value=“4397”>bh5100</option>
											<option value=“4398”>bh2100</option>
											<option value=“4399”>Acl_Advace</option>
											<option value=“4400”>泰莱I时间分辨免疫荧光仪</option>
											<option value=“4401”>RapidLab850</option>
											<option value=“4402”>CLC385</option>
											<option value=“5000”>Authos 2000</option>
											<option value=“5001”>MK3</option>
											<option value=“5002”>凉洲人民医院PCR</option>
											<option value=“5003”>MEK7222</option>
											<option value=“5004”>CA620</option>
											<option value=“5005”>abl5</option>
											<option value=“5006”>LBY-NJ</option>
											<option value=“5007”>IQ200</option>
											<option value=“5008”>Glorunner</option>
											<option value=“5009”>深圳美侨</option>
											<option value=“5010”>mejer600</option>
											<option value=“5011”>FA-11</option>
											<option value=“5012”>Hlc-G7糖化血红蛋白</option>
											<option value=“5013”>RMP</option>
											<option value=“5014”>EA07</option>
											<option value=“5015”>GA05</option>
											<option value=“5016”>Bayer644</option>
											<option value=“5017”>GF-D200</option>
											<option value=“5018”>Ca530血球</option>
											<option value=“5019”>Ca620 血球</option>
											<option value=“5020”>XD685</option>
											<option value=“5021”>Bayer860</option>
											<option value=“5022”>AFT500D</option>
											<option value=“5023”>BAYER60</option>
											<option value=“5024”>GEM3000</option>
											<option value=“5025”>bio－rad evolis</option>
											<option value=“5026”>深圳航创9886</option>
											<option value=“5027”>SM-3</option>
											<option value=“5028”>ZS-3</option>
											<option value=“5029”>SN695B型γ测定仪</option>
											<option value=“5030”>Wallace vitor 1420</option>
											<option value=“5031”>Humareader</option>
											<option value=“5032”>Microtech 648ISO</option>
											<option value=“5033”>HC988</option>
											<option value=“5034”>酶标仪 RT2100C</option>

										</SELECT></td>
									<td width="7"></td>
								</tr>
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
								<tr>
									<td width="7"></td>
									<td>有效期始于:</td>
									<td width="5"></td>
									<td width="111"><asp:textbox id="txtDateBegins" runat="server" Width="101px"></asp:textbox></td>
									<td width="40"></td>
									<td>有效期至于:</td>
									<td width="5"></td>
									<td><asp:textbox id="txtDateEnds" runat="server" Width="113px"></asp:textbox></td>
									<td width="7"></td>
								</tr>
								<tr>
									<td colSpan="9" height="5"></td>
								</tr>
							</table>
						</fieldset>
					</td>
				</tr>
				<tr>
					<td align="center" height="5"><input id="Ok" onclick="ok();" type="button" value="  确定  ">&nbsp;<input onclick="window.close();" type="button" value="  取消  "></td>
				</tr>
				<tr>
					<td>&nbsp;
					</td>
				</tr>
				<tr>
					<td height="5">
						<FIELDSET>
							<LEGEND>生成序列号功能参数说明:</LEGEND>
							<TABLE id="Table1" height="79" cellSpacing="5" cellPadding="1" border="0">
								<TR>
									<TD></TD>
									<TD><STRONG>授权类型</STRONG></TD>
									<TD>商业，临时</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>网卡号</STRONG></TD>
									<TD>可选择</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>主程序开始日&nbsp;</STRONG></TD>
									<TD>可选择传入</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG>主程序到期日&nbsp;</STRONG></TD>
									<TD>可选择传入</TD>
								</TR>
								<TR>
									<TD></TD>
									<TD><STRONG></STRONG></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</FIELDSET></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
