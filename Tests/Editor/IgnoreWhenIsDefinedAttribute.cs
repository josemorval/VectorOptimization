﻿using UnityEditor;
using NUnit.Framework;

namespace OptimizationTools.Tests
{
  public class IgnoreWhenIsDefinedAttribute : System.Attribute, ITestAction
  {
    private readonly string scriptingSymbol;

    public IgnoreWhenIsDefinedAttribute (string scriptingSymbol)
    {
      this.scriptingSymbol = scriptingSymbol;
    }

    public void BeforeTest (TestDetails testDetails)
    {
      string[] currentScriptingSymbols = EditorUserBuildSettings.activeScriptCompilationDefines;

      foreach (string currentScriptingSymbol in currentScriptingSymbols)
      {
        if (scriptingSymbol == currentScriptingSymbol)
        {
          Assert.Ignore("Test ignored when {0} is defined", scriptingSymbol);
          return;
        }
      }
    }

    public void AfterTest (TestDetails testDetails) {}

    public ActionTargets Targets { get; private set; }
  }
}