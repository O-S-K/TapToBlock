using UnityEngine;
using SS.View;
using TMPro;

public enum PopupType
{
    OK,
    YES_NO
}

public class PopupData
{
    public string title { get; set; }
    public string text { get; set; }
    public PopupType type { get; set; }
    public Manager.Callback onOk { get; set; }
    public Manager.Callback onCancel { get; set; }

    public PopupData(string title, string text, PopupType type, Manager.Callback onOk = null, Manager.Callback onCancel = null) : base()
    {
        this.title = title;
        this.text = text;
        this.type = type;
        this.onOk = onOk;
        this.onCancel = onCancel;
    }

    public PopupData(string text, PopupType type, Manager.Callback onOk = null, Manager.Callback onCancel = null) : this("Thông báo", text, type, onOk, onCancel)
    {
    }
}

public class PopupController : Controller
{
    [SerializeField] protected TextMeshProUGUI m_Title;
    [SerializeField] protected TextMeshProUGUI m_Text;
    [SerializeField] protected GameObject m_OkButton;
    [SerializeField] protected GameObject m_YesButton;
    [SerializeField] protected GameObject m_NoButton;

    protected PopupData m_PopupData;
    protected bool m_OkTapped;

    public override string SceneName()
    {
        return "Popup";
    }

    public override void OnActive(object data)
    {
        base.OnActive(data);

        if (data != null)
        {
            m_PopupData = (PopupData)data;

            switch (m_PopupData.type)
            {
                case PopupType.OK:
                    m_OkButton.SetActive(true);
                    m_YesButton.SetActive(false);
                    m_NoButton.SetActive(false);
                    break;
                case PopupType.YES_NO:
                    m_OkButton.SetActive(false);
                    m_YesButton.SetActive(true);
                    m_NoButton.SetActive(true);
                    break;
            }

            m_Text.text = m_PopupData.text;
        }
        else
        {
            m_PopupData = null;
        }
    }

    public override void OnHidden()
    {
        if (m_OkTapped)
        {
            if (m_PopupData != null && m_PopupData.onOk != null)
            {
                m_PopupData.onOk();
            }
        }
        else
        {
            if (m_PopupData != null && m_PopupData.onCancel != null)
            {
                m_PopupData.onCancel();
            }
        }
    }

    public override void OnKeyBack()
    {
        CancelButtonTap();
    }

    public virtual void OkButtonTap()
    {
        Manager.Close();
        m_OkTapped = true;

    }

    public virtual void CancelButtonTap()
    {
        Manager.Close();
        m_OkTapped = false;
    }
}
