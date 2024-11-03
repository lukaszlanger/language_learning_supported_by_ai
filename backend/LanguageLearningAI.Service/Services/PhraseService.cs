﻿using LanguageLearningAI.Core.Dtos;
using LanguageLearningAI.Core.Services;
using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Domain.Repositories;

namespace LanguageLearningAI.Service.Services
{
    public class PhraseService : IPhraseService
    {
        private readonly IPhraseRepository _phraseRepository;

        public PhraseService(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public async Task<IEnumerable<PhraseDto>> GetAllPhrasesAsync()
        {
            var phrases = await _phraseRepository.GetAllAsync();
            return phrases.Select(p => new PhraseDto
            {
                Id = p.Id,
                Text = p.Text,
                Translation = p.Translation
            });
        }

        public async Task<PhraseDto> GetPhraseByIdAsync(int id)
        {
            var phrase = await _phraseRepository.GetByIdAsync(id);
            if (phrase == null)
                return null;

            return new PhraseDto
            {
                Id = phrase.Id,
                Text = phrase.Text,
                Translation = phrase.Translation
            };
        }

        public async Task<string> GetTranslationAsync(int id)
        {
            var phrase = await _phraseRepository.GetByIdAsync(id);
            return phrase?.Translation;
        }

        public async Task AddPhraseAsync(PhraseCreateDto phraseDto)
        {
            var phrase = new Phrase
            {
                Text = phraseDto.Text,
                Translation = phraseDto.Translation
            };
            await _phraseRepository.AddAsync(phrase);
        }
    }
}