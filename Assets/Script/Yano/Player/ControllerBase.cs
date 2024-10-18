

//����ҁ@���
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerBase : MonoBehaviour
{
    //�R���g���[���[�\����
    public enum ControllerButton
    { 
        Button_A,
        Button_B, 
        Button_X,
        Button_Y,
        Button_Menu,
        Button_L,
        Button_R,
        Stick,
        Button_Up,
        Button_Down,
        Button_Left,
        Button_Right,
        DoNot,
    }
    private Vector2 Stick_Left;        //�X�e�B�b�N�̒l���i�[
    //�ϐ�
    private ControllerButton m_button = ControllerButton.DoNot;
    private void Start()
    {
        //������
        Stick_Left = Vector2.zero;
    }
    private void Update()
    {
    }
    //=======================================
    //Z�L�[�AEnter�L�[�����������ɌĂяo��
    //A�{�^�������������ɌĂяo��
    //=======================================
    public void Button_A(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_A);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        
        Debug.Log("Z�L�[�AEnter�L�[,A�{�^��");
    }
    //=======================================
    //X�L�[�ABackSpace�L�[�����������ɌĂяo��
    //B�{�^�������������ɌĂяo��
    //=======================================
    public void Button_B(InputAction.CallbackContext context)
    {
        
       
        Debug.Log("X�L�[�ABackSpace�L�[,B�{�^��");
    }
    //=======================================
    //C�L�[�����������ɌĂяo��
    //X�{�^�������������ɌĂяo��
    //=======================================
    public void Button_X(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_X);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        
        Debug.Log("C�L�[,X�{�^��");
    }
    //=======================================
    //V�L�[�����������ɌĂяo��
    //Y�{�^�������������ɌĂяo��
    //=======================================
    public void Button_Y(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_Y);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        
        Debug.Log("V�L�[�AY�{�^��");
    }
    //=======================================
    //Esc�L�[�����������ɌĂяo��
    //���j���[�{�^�������������ɌĂяo��
    //=======================================
    public void Button_Menu(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_Menu);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        
        Debug.Log("Esc�L�[�A,���j���[�{�^��");
    }
    //=======================================
    //Q�L�[�����������ɌĂяo��
    //L�{�^�������������ɌĂяo��
    //=======================================
    public void Button_L(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_L);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        
        Debug.Log("Q�L�[�A,L�{�^��");
    }
    //=======================================
    //E�L�[�����������ɌĂяo��
    //R�{�^�������������ɌĂяo��
    //=======================================
    public void Button_R(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_R);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        
        Debug.Log("E�L�[�AR�{�^��");
    }
    //=======================================
    //���L�[�����������ɌĂяo��
    //���X�e�B�b�N�𓮂������Ƃ��ɌĂяo��
    //=======================================
    public void Stick(InputAction.CallbackContext context)
    {
        //�������̎���
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Stick);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        //�ړ��A�N�V�����̓��͎擾
        Stick_Left = context.ReadValue<Vector2>();
        //Debug.Log("���,���X�e�B�b�N");

    }
    //=======================================
    //W�L�[�����������ɌĂяo��
    //��{�^�������������ɌĂяo��
    //=======================================
    public void Button_Up(InputAction.CallbackContext context)
    {
      
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_Up);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        Debug.Log("W�L�[�A��{�^��");
    }
    //=======================================
    //S�L�[�����������ɌĂяo��
    //���{�^�������������ɌĂяo��
    //=======================================
    public void Button_Down(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_Down);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
       

        Debug.Log("S�L�[�A���{�^��");
    }
    //=======================================
    //A�L�[�����������ɌĂяo��
    //���{�^�������������ɌĂяo��
    //=======================================
    public void Button_Left(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_Left);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
        Debug.Log("A�L�[�A���{�^��");
    }
    //=======================================
    //D�L�[�����������ɌĂяo��
    //�E�{�^�������������ɌĂяo��
    //=======================================
    public void Button_Right(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.Button_Right);
                break;
            case InputActionPhase.Canceled:
                //�{�^���������ꂽ
                ButtonChange(ControllerButton.DoNot);
                break;
        }
       
        Debug.Log("D�L�[�A�E�{�^��");
    }

    public void ButtonChange(ControllerButton newbotton)
    {
        m_button = newbotton;
    }
    public Vector2 GetStick()
    {
        return Stick_Left; 
    }
    public ControllerButton GetButton()
    {
        return m_button;
    }
}
