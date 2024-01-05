using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

[TestFixture]
public class ProgressBarTests
{
    [Test]
    public void GetCurrentFill_CorrectFillAmount()
    {
        ProgressBar progressBar = new GameObject().AddComponent<ProgressBar>();
        progressBar.maximum = 100f;
        progressBar.current = 50f;
        progressBar.mask = progressBar.gameObject.AddComponent<Image>();

        progressBar.GetCurrentFill();

        Assert.AreEqual(0.5f, progressBar.mask.fillAmount, 0.01f);
    }

    [Test]
    public void UpdateProgress_IncreaseCurrentWithinRange()
    {
        ProgressBar progressBar = new GameObject().AddComponent<ProgressBar>();
        progressBar.maximum = 100f;
        progressBar.current = 50f;

        progressBar.UpdateProgress(30f);

        Assert.AreEqual(80f, progressBar.current, 0.01f);
    }

    [Test]
    public void UpdateProgress_DoNotExceedMaximum()
    {
        ProgressBar progressBar = new GameObject().AddComponent<ProgressBar>();
        progressBar.maximum = 100f;
        progressBar.current = 90f;

        progressBar.UpdateProgress(20f);

        Assert.AreEqual(100f, progressBar.current, 0.01f);
    }
}
