<?xml version="1.0" encoding="iso-8859-1"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
                xmlns="http://schemas.microsoft.com/wix/2006/wi"
                exclude-result-prefixes="xsl wix">

  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />

  <xsl:strip-space elements="*"/>

  <xsl:key name="FilesToRemove"
           match="wix:Component[contains(wix:File/@Source, '.pdb')
                             or contains(wix:File/@Source, '.so')
                             or contains(wix:File/@Source, '.log')
                             or contains(wix:File/@Source, '.dylib')]"
           use="@Id" />

  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="*[self::wix:Component or self::wix:ComponentRef]
                        [key('FilesToRemove', @Id)]" />

  <xsl:template match='wix:Wix/wix:Fragment/wix:DirectoryRef/wix:Component[wix:File[@Source="$(var.FinalFolder)\WinFApp.exe"]]'>
    <xsl:element name="wix:Component">
      <xsl:attribute name="Id">
        <xsl:value-of select="@Id"/>
      </xsl:attribute>
      <xsl:attribute name="Guid">
        <xsl:value-of select="@Guid"/>
      </xsl:attribute>

      <xsl:element name="wix:File">
        <xsl:attribute name="Id">
          <xsl:variable name="FilePath" select="wix:File/@Source" />
          <xsl:variable name="FileName" select="substring-after($FilePath,'\')" />
          <xsl:value-of select="'_application'"/>
        </xsl:attribute>
        <xsl:attribute name="Source">
          <xsl:value-of select="wix:File/@Source"/>
        </xsl:attribute>
        <xsl:attribute name="KeyPath">yes</xsl:attribute>
      </xsl:element>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>