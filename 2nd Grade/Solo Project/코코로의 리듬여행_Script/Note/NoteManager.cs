using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public List<Note>[] noteList = new List<Note>[4];
    public List<NoteBell>[] bellNoteList = new List<NoteBell>[2];

    public List<Transform> LinePosList = new List<Transform>();

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject bellNotePrefab;
    public GameObject longNotePrefab;

    private int[] line = { 0, 1, 2, 3 };


    private void Awake()
    {
        for (int i = 0; i < noteList.Length; i++)
        {
            noteList[i] = new List<Note>();
        }
        for (int i = 0; i < bellNoteList.Length; i++)
        {
            bellNoteList[i] = new List<NoteBell>();
        }
    }

    public void CreateNotes(TextAsset ChaeboFile) //ä�� �ؽ�Ʈ ����
    {
        if (Information.Instance.OptionData.IsRandom)
            RandomLine(); //�̰� ���� �ɸ� ���� �����ִ� �ڵ�

        string[] notes = ChaeboFile.ToString().Split('\n');
        //1�ٿ� 1��Ʈ�ϱ� �迭�� Split '\n'���� ������ �־���.

        foreach (string note in notes)
        {
            string[] info = note.Split(','); //���� ä�� ������ �� ��Ұ� ,�� ������ ������ �� string �迭�� ����
            NoteInfo noteinfo = new NoteInfo();//�̰� ����ü, ���� �ȵ����ص� ��
            noteinfo.Line = LineParsing(info[0]);//LineParsing�Լ� ����
            noteinfo.isLong = IsLongParsing(info[3]);//IsLongParsing ����
            noteinfo.gimmick = int.Parse(info[4]);//Ű�� ĭ�� �̿��ؼ� ����߰�(�Ƚᵵ ��)
            noteinfo.ms = int.Parse(info[2]);//��Ʈ�� ms

            if (noteinfo.isLong)
            {
                noteinfo.endms = EndMSParsing(info[5]);//�ճ�Ʈ��� �ճ�Ʈ�� ������ ms�޾ƿ��� EndMSParsing �Լ� ����
            }

            CreateNote(noteinfo);//��Ʈ ������Ʈ �����
        }
    }

    public void CreateBellNote(TextAsset ChaeboFile)
    {
        string[] notes = ChaeboFile.ToString().Split('\n');

        foreach (string note in notes)
        {
            string[] info = note.Split(',');
            NoteInfo noteinfo = new NoteInfo();
            noteinfo.Line = BellLineParsing(info[0]);
            noteinfo.ms = int.Parse(info[2]);

            CreateBellNote(noteinfo);
        }
    }

    private void RandomLine()
    {
        for (int i = 0; i < 1000; i++)
        {
            int rand = Random.Range(0, line.Length);
            int a = line[rand];
            line[rand] = line[0];
            line[0] = a;
        }
    }

    private int LineParsing(string line)
    {//������ ���� ������ �̻��ϰ� �Ǿ�����. �׷��� �� ���ڸ� �츮�� 0, 1, 2, 3���� �ٲ������.
        int intline = int.Parse(line);
        switch (intline)
        {
            case 64:
                return this.line[0];
            case 192:
                return this.line[1];
            case 320:
                return this.line[2];
            case 448:
                return this.line[3];
        }

        return intline;//������ �۵� �������� ������ ������ ����.
    }

    private int BellLineParsing(string line)
    {
        int intline = int.Parse(line);
        switch (intline)
        {
            case 64:
                return 0;
            case 448:
                return 1;
        }
        return intline;
    }

    private bool IsLongParsing(string isLong)
    {//�ճ�Ʈ�� 4��° ��Ұ� 128�� ��������. �װ� �̿��ؼ� bool ������ �Ľ�
        int intIsLong = int.Parse(isLong);
        switch (intIsLong)
        {
            case 128:
                return true;
            default:
                return false;
        }
    }

    private int EndMSParsing(string endms)
    {//�̻��ϰ� �ճ�Ʈ ������ ms�� :0:0:0:0 �̷������� �峭�� �Ǿ�����. ���� ���� ������ ��� �����ϴ�
        //string �迭�� ������ �ʿ��� ���� �Ľ�
        string[] endmses = endms.Split(':');
        return int.Parse(endmses[0]);
    }

    private void CreateNote(NoteInfo noteInfo)
    {
        GameObject newNote = Instantiate(notePrefab);
        Note noteScript = newNote.GetComponent<Note>();

        noteScript.SetNote(noteInfo);
    }

    private void CreateBellNote(NoteInfo noteInfo)
    {
        GameObject newNote = Instantiate(bellNotePrefab);
        NoteBell noteScript = newNote.GetComponent<NoteBell>();

        noteScript.SetNote(noteInfo);
    }
}
