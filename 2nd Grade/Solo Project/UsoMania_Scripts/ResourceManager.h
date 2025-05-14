#pragma once
#include "fmod.hpp"
#pragma comment(lib, "fmod_vc")
enum class SOUND_CHANNEL //���帶�� ä��
{
	BGM, EFFECT, END
};
struct tSoundInfo
{
	FMOD::Sound* pSound; // ���� ���� �޸�
	bool IsLoop;		// ���帶�� ������������
};
class Texture;
class ResourceManager
{
	DECLARE_SINGLE(ResourceManager);
public:
	void Init();
	const wchar_t* GetResPath() const { return m_resourcePath; }
public:
	Texture* TextureLoad(const wstring& _key, const wstring& _path);
	Texture* TextureFind(const wstring& _key);
	void Release();
public:
	void LoadSound(const wstring& _key, const wstring& _path, bool _isLoop);
	void Play(const wstring& _key, SOUND_CHANNEL _channel);
	void Stop(SOUND_CHANNEL _channel);
	void Volume(SOUND_CHANNEL _channel, float _vol);
	void Pause(SOUND_CHANNEL _channel, bool _ispause);
	int GetTime(SOUND_CHANNEL _channel);
public:
private:
	tSoundInfo* FindSound(const wstring& _key);
public:
	void LoadChaebo(const wstring& _key, const wstring& _path);
	wstring* FindChaebo(const wstring& _key);
private:
	wchar_t m_resourcePath[255] = {};
	map<wstring, Texture*> m_mapTextures;
	map<wstring, tSoundInfo*> m_mapSounds;
	map<wstring, wstring*> m_mapChaebos;
	FMOD::System* m_pSoundSystem; // ���� �ý���
	FMOD::Channel* m_pChannel[(UINT)SOUND_CHANNEL::END]; // ����� ä��
};

